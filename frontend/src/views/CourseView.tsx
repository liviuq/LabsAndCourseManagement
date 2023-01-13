import { Text, Accordion, AccordionButton, AccordionIcon, AccordionItem, AccordionPanel, Box, Button, Flex, Heading, IconButton, Menu, MenuButton, MenuItem, MenuList, Table, TableCaption, TableContainer, Tbody, Td, Tfoot, Th, Thead, Tr, Avatar } from '@chakra-ui/react';
import React, { useEffect, useState } from 'react'
import { FiPlus } from 'react-icons/fi';
import { useQuery } from 'react-query';
import { useParams } from 'react-router-dom';
import { useGetCourseById, useGetStudentsByCourseId, useGetTeachersByCourseId } from '../cache/get';
import { StudentsTable, TeachersTable } from '../components';
import { AssignStudentsToCourseModal, AssignTeachersToCourseModal } from '../components/modals';
import { Course, Student, Teacher } from '../entities';


export const CourseView: React.FC = () => {
  let { id } = useParams();
  const [course, setCourse] = useState<Course>();
  const courseData = useQuery([`getCourseBy${id}`], () => useGetCourseById(id ? id : '')).data
  const [teachers, setTeachers] = useState<Teacher[]>();
  const teachersData = useQuery([`getTeachersBy${id}`], () => useGetTeachersByCourseId(id ? id : '')).data
  const [students, setStudents] = useState<Student[]>();
  const studentsData = useQuery([`getStudentsBy${id}`], () => useGetStudentsByCourseId(id ? id : '')).data
  const [isOpenAssignTeachersToCourseModal, setIsOpenAssignTeachersToCourseModal] = useState(false)
  const [isOpenAssignStudentsToCourseModal, setIsOpenAssignStudentsToCourseModal] = useState(false)


  useEffect(() => {
    setStudents(studentsData ? studentsData : [])
    console.log(studentsData)
  }, [studentsData])
  useEffect(() => {
    setTeachers(teachersData ? teachersData : [])
  }, [teachersData])
  useEffect(() => {
    setCourse(courseData ? courseData : undefined)
  }, [courseData])
  return (
    <Flex w="full" direction="column" p="32px" >
      <Flex w="full" justify="space-between" align="center" mb="48px">
        <Flex direction="column">
          <Heading size="lg" color="#3C4048">
            {course?.title}
          </Heading>
          <Text color="gray" fontSize="14px">Semester: {course?.semester}</Text>
          <Text color="gray" fontSize="14px">Credits: {course?.credits}</Text>
        </Flex>
        <Flex gap="12px" >
          <Button onClick={() => setIsOpenAssignTeachersToCourseModal(true)} leftIcon={<FiPlus />} colorScheme="teal" size="sm">Teacher</Button>
          <Button onClick={() => setIsOpenAssignStudentsToCourseModal(true)} leftIcon={<FiPlus />} colorScheme="teal" size="sm">Student</Button>
        </Flex>
      </Flex>

      <Accordion defaultIndex={[1]}>
        <AccordionItem>
          <h2>
            <AccordionButton>
              <Box flex='1' textAlign='left'>
                <Heading color="#3C4048" size="md" >Teachers</Heading>
              </Box>
              <AccordionIcon />
            </AccordionButton>
          </h2>
          <AccordionPanel >
            <TeachersTable course={course ? course : null} teachers={teachers ? teachers : []} />
          </AccordionPanel>
        </AccordionItem>

        <AccordionItem >
          <h2>
            <AccordionButton>
              <Box flex='1' textAlign='left'>
                <Heading color="#3C4048" size="md" >Students</Heading>
              </Box>
              <AccordionIcon />
            </AccordionButton>
          </h2>
          <AccordionPanel>
            <StudentsTable course={course ? course : null} students={students ? students : []} />
          </AccordionPanel>
        </AccordionItem>
      </Accordion>
      <AssignStudentsToCourseModal course={course ? course : null} courseStudents={students ? students.map((student) => student.id) : []} isOpen={isOpenAssignStudentsToCourseModal} onClose={() => setIsOpenAssignStudentsToCourseModal(false)} />
      <AssignTeachersToCourseModal course={course ? course : null} courseTeachers={teachers ? teachers.map((teacher) => teacher.id) : []} isOpen={isOpenAssignTeachersToCourseModal} onClose={() => setIsOpenAssignTeachersToCourseModal(false)} />
    </Flex >
  )
}

