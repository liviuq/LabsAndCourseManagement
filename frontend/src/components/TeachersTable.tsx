import { Text, TableContainer, Table, TableCaption, Thead, Tr, Th, Tbody, Td, Flex, Avatar, Menu, MenuButton, IconButton, MenuList, MenuItem } from '@chakra-ui/react';
import React, { useState } from 'react'
import { FiX } from 'react-icons/fi';
import { RxDotsHorizontal } from 'react-icons/rx';
import { Course, Teacher } from '../entities';
import { DeleteTeacherFromCourseAlert } from './alerts/DeleteTeacherFromCourseAlert';

interface TeachersTableProps {
  course: Course | null;
  teachers: Teacher[];
}

export const TeachersTable: React.FC<TeachersTableProps> = ({ course, teachers }) => {
  const [isOpenRemoveCourseFromCourseModal, setIsOpenRemoveCourseFromCourseModal] = useState(false);
  const [selectedTeacher, setSelectedTeacher] = useState<Teacher>(Teacher.of({}));

  const handleRemove = (t: Teacher) => {
    setSelectedTeacher(t);
    setIsOpenRemoveCourseFromCourseModal(true);
  }
  return (
    <TableContainer>
      <Table variant='striped' size="md">
        <TableCaption></TableCaption>
        <Thead>
          <Tr>
            <Th>Name</Th>
            <Th>Email</Th>
            <Th>Teaching Degree</Th>
            <Th></Th>
          </Tr>
        </Thead>
        <Tbody>
          {
            teachers?.map(teacher =>
              <Tr key={teacher.id}>
                <Td>
                  <Flex align="center" gap="12px">
                    <Avatar size="sm" bg="#00ABB3" />
                    <Text>
                      {teacher.firstName} {teacher.lastName}
                    </Text>
                  </Flex>
                </Td>
                <Td>{teacher.email} </Td>
                <Td>{teacher.teachingDegree}</Td>
                <Td>
                  <Menu placement='bottom-end'>
                    <MenuButton as={IconButton} variant="ghost" size="sm" icon={<RxDotsHorizontal size="18px" />} />
                    <MenuList py="0px" overflow="hidden" borderRadius="12px">
                      <MenuItem onClick={() => { handleRemove(teacher) }}>
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
      <DeleteTeacherFromCourseAlert course={course} teacher={selectedTeacher} isOpen={isOpenRemoveCourseFromCourseModal} onClose={() => setIsOpenRemoveCourseFromCourseModal(false)} />
    </TableContainer>
  )
}

