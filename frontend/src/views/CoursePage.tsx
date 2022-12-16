import { Flex, Heading, Image, Box, Button, Avatar, Text } from '@chakra-ui/react'
import React, { useEffect, useState } from 'react'
import { Header } from '../shared'
import welcomeImg from "../assets/welcome.svg";
import Axios from 'axios';
import { Course } from '../entities/Course';

export const CoursePage: React.FC = () => {
  const [teachers, setTeachers] = useState([])
  const [didactic, setDidactic] = useState([])
  const [courses, setCourses] = useState<Course[]>([])
  const [course, setCourse] = useState<Course>()
  const [selectedTeachers, setSelectedTeachers] = useState([])

  useEffect(() => {
    fetchCourses();
    fetchDidactic();
    fetchTeachers();
    console.log(course)
  }, [courses])
  const fetchCourses = () => {
    Axios.get("https://localhost:7202/api/Courses").then((res) =>
      setCourses(res.data)
    ).then(() => { setCourse(courses[courses.length - 1]) })
  }
  const fetchDidactic = () => {
    Axios.get("https://localhost:7202/api/Didactic").then((res) =>
      setDidactic(res.data)
    ).then(() => {
      if (selectedTeachers.length == 0) {
        didactic.map((d) => {
          teachers.map((t) => {
            if (d.teacherId == t.id && d.courseId == course.id) {
              setSelectedTeachers(selectedTeachers.concat(t))
            }
          })
        })
      }
    })
  }
  const fetchTeachers = () => {
    Axios.get("https://localhost:7202/api/Teachers").then((res) =>
      setTeachers(res.data)
    )
  }
  return (
    <>
      <Header />
      <Flex w="full" h="full">
        <Flex w="full" direction="column" align="center" justifyContent="center" mb="200px">
          <Heading fontSize="82px" textAlign="center" color="#00ABB3" mb="48px">{course?.title}</Heading>
          <Flex w="500px" direction="column" gap="12px">
            {
              selectedTeachers.map((teacher: any) => {
                return <Flex key={teacher.id} w="full" border="1px solid #D0D5DD" align="center" bg="white" borderRadius="12px" p="16px" boxShadow="0px 1px 2px rgba(16, 24, 40, 0.05)" justify="space-between">
                  <Flex gap="12px" align="center">
                    <Avatar boxSize="40px" />
                    <Flex direction="column" gap="0" >
                      <Text fontSize="16px" fontWeight="bold">{teacher.firstName + " " + teacher.lastName}</Text>
                      <Text fontSize="12px">{teacher.email}</Text>
                    </Flex>
                  </Flex>
                </Flex>
              })
            }
          </Flex>
        </Flex>
      </Flex>
    </>
  )
}

export default CoursePage
