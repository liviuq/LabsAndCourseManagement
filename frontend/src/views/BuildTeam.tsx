import { Avatar, Button, Checkbox, Flex, Heading, Image, Text } from '@chakra-ui/react'
import React, { useEffect, useState } from 'react'
import { Header } from '../shared'
import buildTeamImg from "../assets/buildTeam.svg";
import { EntityCard } from '../components';
import Axios from 'axios';
import { FiPlus } from 'react-icons/fi';
import { CreateTeacherModal } from '../components/CreateTeacherModal';
import { Course } from '../entities/Course';
import { useNavigate } from 'react-router-dom';

export const BuildTeam: React.FC = () => {
  const [teachers, setTeachers] = useState([]);
  const [createModalOpen, setCreateModalOpen] = useState(false);
  const [selectedTeachers, setSelectedTeachers] = useState([]);
  const [courses, setCourses] = useState<Course[]>([])
  const navigate = useNavigate();
  useEffect(() => {
    fetchCourses();
  }, [courses])
  const fetchCourses = () => {
    Axios.get("https://localhost:7202/api/courses").then((res) =>
      setCourses(res.data)
    )
  }

  useEffect(() => {
    console.log(selectedTeachers)
  }, [selectedTeachers])
  useEffect(() => {
    fetchTeachers();
  }, [createModalOpen])
  const fetchTeachers = () => {
    Axios.get("https://localhost:7202/api/teachers").then((res) =>
      setTeachers(res.data)
    )
  }
  const handleContinue = (e: any) => {
    const courseId = courses[courses.length - 1].id
    console.log(courseId)
    selectedTeachers.map((id) => {
      Axios({
        method: "POST",
        url: `https://localhost:7202/api/Didactic/teacher/${id}/course/${courseId}`,
      });
    })
    navigate('/course')
  }
  return (
    <>
      <Header />
      <Flex w="full" h="full">
        <Flex w="full" direction="column" align="center" justifyContent="center" mb="200px">
          <Heading fontSize="34px" textAlign="center" color="#00ABB3" mb="48px">Assign some teachers to this course</Heading>
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
            <Button onClick={() => { setCreateModalOpen(true) }} leftIcon={<FiPlus size="18px" />} variant="outline" colorScheme="teal" iconSpacing="6px" alignItems="center">
              <Text mb="2.8px">Add new</Text>
            </Button>
            <Button onClick={handleContinue} variant="solid" colorScheme="teal" >
              <Text mb="2.8px">Continue</Text>
            </Button>
          </Flex>
        </Flex>
        <CreateTeacherModal isOpen={createModalOpen} setIsOpen={setCreateModalOpen} />
      </Flex>
    </>
  )
}
