import { Text, Modal, ModalOverlay, ModalContent, ModalHeader, ModalCloseButton, ModalBody, ModalFooter, Button, Avatar, Checkbox, Flex } from '@chakra-ui/react';
import React, { useEffect, useState } from 'react'
import { useQuery, useQueryClient } from 'react-query';
import { useCreateDidactic } from '../../cache/create';
import { useGetTeachers } from '../../cache/get';
import { Course, Teacher } from '../../entities';
import { useToast } from '../../hooks';

interface AssignTeachersToCourseModalProps {
  course: Course | null;
  courseTeachers: string[];
  isOpen: boolean;
  onClose: () => void;
}

export const AssignTeachersToCourseModal: React.FC<AssignTeachersToCourseModalProps> = ({ course, courseTeachers, isOpen, onClose }) => {
  const toast = useToast();
  const [teachers, setTeachers] = useState<Teacher[]>([])
  const [selectedTeachers, setSelectedTeachers] = useState<string[]>(courseTeachers);
  const teachersData = useQuery(['getTeachers'], () => useGetTeachers()).data
  const [errors, setErrors] = useState<number>(0);
  const queryClient = useQueryClient();
  const createDidactic = useCreateDidactic(
    () => setErrors(prev => prev + 1),
    course ? course.id : ''
  );
  useEffect(() => {
    setTeachers(teachersData ? teachersData : [])
  }, [teachersData])

  const handleSubmit = (e: any) => {
    if (selectedTeachers.length === 0) {
      toast({
        title: 'Assign at least 1 teacher.',
        status: 'warning',
      })
    } else {
      selectedTeachers.map((id) => {
        createDidactic.mutate({ id: id })
      })
      if (!errors) {
        toast({
          title: 'Teacher/s assigned successfuly.',
          status: 'success',
        })
        queryClient.invalidateQueries([`getTeachersBy${course?.id}`]);
        setSelectedTeachers([])
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
        <ModalHeader>Assign teachers to {course?.title}</ModalHeader>
        <ModalCloseButton />
        <ModalBody>
          <Flex w="full" direction="column" gap="15px">
            {
              teachers.map((teacher) => courseTeachers.includes(teacher.id) ? null :
                <Flex key={teacher.id} w="full" border="1px solid #D0D5DD" align="center" bg="white" borderRadius="12px" p="16px" boxShadow="0px 1px 2px rgba(16, 24, 40, 0.05)" justify="space-between">
                  <Flex gap="12px" align="center">
                    <Avatar boxSize="40px" />
                    <Flex direction="column" gap="0" >
                      <Text fontSize="16px" fontWeight="bold">{teacher.firstName + " " + teacher.lastName}</Text>
                      <Text fontSize="12px">{teacher.email}</Text>
                    </Flex>
                  </Flex>
                  <Checkbox onChange={(e: any) => {
                    if (e.target.checked) {
                      setSelectedTeachers(selectedTeachers.concat(teacher.id))
                    } else {
                      selectedTeachers.forEach((element, index) => {
                        if (element == teacher.id) selectedTeachers.splice(index, 1);
                      });
                      setSelectedTeachers(selectedTeachers)
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

