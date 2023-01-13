import { Text, Modal, ModalOverlay, ModalContent, ModalHeader, ModalCloseButton, ModalBody, ModalFooter, Button, Avatar, Checkbox, Flex } from '@chakra-ui/react';
import React, { useEffect, useState } from 'react'
import { useQuery, useQueryClient } from 'react-query';
import { useCreateEnrollment } from '../../cache/create';
import { useGetStudents } from '../../cache/get';
import { Course, Student } from '../../entities';
import { useToast } from '../../hooks';

interface AssignStudentsToCourseModalProps {
  course: Course | null;
  courseStudents: string[];
  isOpen: boolean;
  onClose: () => void;
}

export const AssignStudentsToCourseModal: React.FC<AssignStudentsToCourseModalProps> = ({ course, courseStudents, isOpen, onClose }) => {
  const toast = useToast();
  const [students, setStudents] = useState<Student[]>([])
  const [selectedStudents, setSelectedStudents] = useState<string[]>(courseStudents);
  const studentsData = useQuery(['getStudents'], () => useGetStudents()).data
  const [errors, setErrors] = useState<number>(0);
  const queryClient = useQueryClient();
  const createEnrollment = useCreateEnrollment(
    () => setErrors(prev => prev + 1),
    course ? course.id : ''
  );
  useEffect(() => {
    setStudents(studentsData ? studentsData : [])
  }, [studentsData])

  const handleSubmit = (e: any) => {
    if (selectedStudents.length === 0) {
      toast({
        title: 'Assign at least 1 student.',
        status: 'warning',
      })
    } else {
      selectedStudents.map((id) => {
        createEnrollment.mutate({ id: id })
      })
      if (!errors) {
        toast({
          title: 'Student/s assigned successfuly.',
          status: 'success',
        })
        queryClient.invalidateQueries([`getStudentsBy${course?.id}`]);
        setSelectedStudents([])
        onClose()
      } else {
        toast({
          title: 'error',
          status: 'error',
        })
      }
      setErrors(0)
    }
  }
  return (
    <Modal isOpen={isOpen} onClose={onClose}>
      <ModalOverlay />
      <ModalContent>
        <ModalHeader>Assign students to {course?.title}</ModalHeader>
        <ModalCloseButton />
        <ModalBody>
          <Flex w="full" direction="column" gap="15px">
            {
              students.map((student) => courseStudents.includes(student.id) ? null :
                <Flex key={student.id} w="full" border="1px solid #D0D5DD" align="center" bg="white" borderRadius="12px" p="16px" boxShadow="0px 1px 2px rgba(16, 24, 40, 0.05)" justify="space-between">
                  <Flex gap="12px" align="center">
                    <Avatar boxSize="40px" />
                    <Flex direction="column" gap="0" >
                      <Text fontSize="16px" fontWeight="bold">{student.firstName + " " + student.lastName}</Text>
                      <Text fontSize="12px">{student.email}</Text>
                    </Flex>
                  </Flex>
                  <Checkbox onChange={(e: any) => {
                    if (e.target.checked) {
                      setSelectedStudents(selectedStudents.concat(student.id))
                    } else {
                      selectedStudents.forEach((element, index) => {
                        if (element == student.id) selectedStudents.splice(index, 1);
                      });
                      setSelectedStudents(selectedStudents)
                    }
                  }} />
                </Flex>

              )
            }
          </Flex>
        </ModalBody>

        <ModalFooter>
          <Button onClick={onClose} size="md" variant="solid" mr="10px">
            Cancel
          </Button>
          <Button onClick={handleSubmit} size="md" variant="solid" colorScheme="teal">Submit</Button>
        </ModalFooter>
      </ModalContent>
    </Modal>
  )
}

