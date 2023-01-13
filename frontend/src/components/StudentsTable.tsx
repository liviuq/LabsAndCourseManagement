import { Text, TableContainer, Table, TableCaption, Thead, Tr, Th, Tbody, Td, Flex, Avatar, Menu, MenuButton, IconButton, MenuList, MenuItem } from '@chakra-ui/react';
import React, { useState } from 'react'
import { FiX } from 'react-icons/fi';
import { FaPenFancy } from 'react-icons/fa';
import { RxDotsHorizontal } from 'react-icons/rx';
import { Course, Grade, Student } from '../entities';
import { DeleteStudentFromCourseAlert } from './alerts';
import { CreateStudentGradeModal } from './forms';

interface StudentsTableProps {
  course: Course | null;
  students: Student[];
}

export const StudentsTable: React.FC<StudentsTableProps> = ({ course, students }) => {
  const [isOpenRemoveStudentFromCourseModal, setIsOpenRemoveStudentFromCourseModal] = useState(false);
  const [isOpenGradeStudentFromCourseModal, setIsOpenGradeStudentFromCourseModal] = useState(false);
  const [selectedStudent, setSelectedStudent] = useState<Student>(Student.of({}));

  const handleGrade = (s: Student) => {
    setSelectedStudent(s);
    setIsOpenGradeStudentFromCourseModal(true);
  }
  const handleRemove = (s: Student) => {
    setSelectedStudent(s);
    setIsOpenRemoveStudentFromCourseModal(true);
  }
  return (
    <TableContainer>
      <Table variant='striped' size="md">
        <TableCaption></TableCaption>
        <Thead>
          <Tr>
            <Th>Name</Th>
            <Th>Email</Th>
            <Th>Semester</Th>
            <Th>Group</Th>
            <Th isNumeric>Scholarship</Th>
            <Th>Grades</Th>
            <Th></Th>
          </Tr>
        </Thead>
        <Tbody>
          {
            students?.map(student =>
              <Tr key={student.id}>
                <Td>
                  <Flex align="center" gap="12px">
                    <Avatar size="sm" bg="#00ABB3" />
                    <Text>
                      {student.firstName} {student.lastName}
                    </Text>
                  </Flex>
                </Td>
                <Td>{student.email} </Td>
                <Td>{student.semester}</Td>
                <Td>{student.group}</Td>
                <Td isNumeric>{student.scholarship}</Td>
                <Td>{student.grades.map((grade: Grade) => grade.value.toString() + ' ')}</Td>
                <Td>
                  <Menu placement='bottom-end'>
                    <MenuButton as={IconButton} variant="ghost" size="sm" icon={<RxDotsHorizontal size="18px" />} />
                    <MenuList py="0px" overflow="hidden" borderRadius="12px">
                      <MenuItem onClick={() => { handleGrade(student) }}>
                        <Flex align="center" gap="12px">
                          <FaPenFancy />
                          <Text fontWeight="500">Grade</Text>
                        </Flex>
                      </MenuItem>
                      <MenuItem onClick={() => { handleRemove(student) }}>
                        <Flex align="center" gap="12px">
                          <FiX color="red" />
                          <Text color="red" fontWeight="500">Remove</Text>
                        </Flex>
                      </MenuItem>
                    </MenuList>
                  </Menu>
                </Td>
              </Tr>
            )
          }
        </Tbody>
      </Table>
      <CreateStudentGradeModal courseId={course?.id ? course.id : ''} studentId={selectedStudent.id} isOpen={isOpenGradeStudentFromCourseModal} setIsOpen={setIsOpenGradeStudentFromCourseModal} />
      <DeleteStudentFromCourseAlert course={course} student={selectedStudent} isOpen={isOpenRemoveStudentFromCourseModal} onClose={() => setIsOpenRemoveStudentFromCourseModal(false)} />
    </TableContainer>
  )
}

