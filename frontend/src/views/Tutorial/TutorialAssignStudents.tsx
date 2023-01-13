import { Avatar, Button, Checkbox, Flex, Heading, Image, Text } from '@chakra-ui/react'
import React, { useEffect, useState } from 'react'
import { Header } from '../../shared'
import buildTeamImg from "../../assets/buildTeam.svg";
import { FiPlus } from 'react-icons/fi';
import { useQuery } from 'react-query';
import { Student } from '../../entities';
import { useGetStudents } from '../../cache/get';
import { useToast } from '../../hooks';
import { useCreateEnrollment } from '../../cache/create';
import { CreateStudentModal } from '../../components/forms';

interface TutorialAssignStudentsProps {
  nextStep: () => void,
  courseId: string,
}

export const TutorialAssignStudents: React.FC<TutorialAssignStudentsProps> = ({ nextStep, courseId }) => {
  const toast = useToast();
  const [isOpenCreateStudentModal, setIsOpenCreateStudentModal] = useState(false);
  const [students, setStudents] = useState<Student[]>([])
  const studentsData = useQuery(['getStudents'], () => useGetStudents()).data
  const [selectedStudents, setSelectedStudents] = useState([]);
  const [errors, setErrors] = useState<number>(0);
  const createEnrollment = useCreateEnrollment(
    () => setErrors(prev => prev + 1),
    courseId
  );

  useEffect(() => {
    setStudents(studentsData ? studentsData : [])
  }, [studentsData])

  const handleFinish = (e: any) => {
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
        nextStep()
      } else {
        toast({
          title: 'error',
          status: 'error',
        })
      }
    }
  }
  return (
    <>
      <Header />
      <Flex w="full" h="full">
        <Flex w="full" direction="column" align="center" justifyContent="center" mb="200px">
          <Heading fontSize="34px" textAlign="center" color="#00ABB3" mb="48px">Assign some students</Heading>
          <Image boxSize="200px" src={buildTeamImg} alt="" />
          <Flex w="500px" direction="column" gap="12px">
            {
              students.map((student: any) => {
                return <Flex key={student.id} w="full" border="1px solid #D0D5DD" align="center" bg="white" borderRadius="12px" p="16px" boxShadow="0px 1px 2px rgba(16, 24, 40, 0.05)" justify="space-between">
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
              })
            }
          </Flex>
          <Flex mt="20px" gap="20px">
            <Button onClick={() => { setIsOpenCreateStudentModal(true) }} leftIcon={<FiPlus size="18px" />} variant="outline" colorScheme="teal" iconSpacing="6px" alignItems="center">
              <Text mb="2.8px">Add new</Text>
            </Button>
            <Button onClick={handleFinish} variant="solid" colorScheme="teal" >
              <Text mb="2.8px">Submit</Text>
            </Button>
          </Flex>
        </Flex>
        <CreateStudentModal isOpen={isOpenCreateStudentModal} setIsOpen={setIsOpenCreateStudentModal} />
      </Flex>
    </>
  )
}
