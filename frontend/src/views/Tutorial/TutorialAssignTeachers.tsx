import { Avatar, Button, Checkbox, Flex, Heading, Image, Text } from '@chakra-ui/react'
import React, { useEffect, useState } from 'react'
import { Header } from '../../shared'
import buildTeamImg from "../../assets/buildTeam.svg";
import { FiPlus } from 'react-icons/fi';
import { useQuery } from 'react-query';
import { Teacher } from '../../entities';
import { useGetTeachers } from '../../cache/get';
import { useToast } from '../../hooks';
import { useCreateDidactic } from '../../cache/create';
import { CreateTeacherModal } from '../../components/forms';

interface TutorialAssignTeachersProps {
  nextStep: () => void,
  courseId: string,
}

export const TutorialAssignTeachers: React.FC<TutorialAssignTeachersProps> = ({ nextStep, courseId }) => {
  const toast = useToast();
  const [isOpenCreateTeacherModal, setIsOpenCreateTeacherModal] = useState(false);
  const [teachers, setTeachers] = useState<Teacher[]>([])
  const teachersData = useQuery(['getTeachers'], () => useGetTeachers()).data
  const [selectedTeachers, setSelectedTeachers] = useState<string[]>([]);
  const [errors, setErrors] = useState<number>(0);
  const createDidactic = useCreateDidactic(
    () => setErrors(prev => prev + 1),
    courseId
  );

  useEffect(() => {
    setTeachers(teachersData ? teachersData : [])
  }, [teachersData])

  const handleFinish = (e: any) => {
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
        setSelectedTeachers([])
        nextStep()
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
    <>
      <Header />
      <Flex w="full" h="full">
        <Flex w="full" direction="column" align="center" justifyContent="center" mb="200px">
          <Heading fontSize="34px" textAlign="center" color="#00ABB3" mb="48px">Assign some teachers</Heading>
          <Image boxSize="200px" src={buildTeamImg} alt="" />
          <Flex w="500px" direction="column" gap="12px">
            {
              teachers.map((teacher: any) => {
                return <Flex key={teacher.id} w="full" border="1px solid #D0D5DD" align="center" bg="white" borderRadius="12px" p="16px" boxShadow="0px 1px 2px rgba(16, 24, 40, 0.05)" justify="space-between">
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
              })
            }
          </Flex>
          <Flex mt="20px" gap="20px">
            <Button onClick={() => { setIsOpenCreateTeacherModal(true) }} leftIcon={<FiPlus size="18px" />} variant="outline" colorScheme="teal" iconSpacing="6px" alignItems="center">
              <Text mb="2.8px">Add new</Text>
            </Button>
            <Button onClick={handleFinish} variant="solid" colorScheme="teal" >
              <Text mb="2.8px">Submit</Text>
            </Button>
          </Flex>
        </Flex>
        <CreateTeacherModal isOpen={isOpenCreateTeacherModal} setIsOpen={setIsOpenCreateTeacherModal} />
      </Flex>
    </>
  )
}
