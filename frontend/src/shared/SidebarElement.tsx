import { Button, Flex, IconButton, Menu, MenuButton, MenuItem, MenuList, Text } from '@chakra-ui/react'
import React, { useState } from 'react'
import { FiEdit3, FiTrash2 } from 'react-icons/fi'
import { RxDotsHorizontal } from 'react-icons/rx'
import { useNavigate } from 'react-router-dom'
import { DeleteCourseAlert } from '../components/alerts'
import { CreateCourseModal, UpdateCourseModal } from '../components/forms'
import { Course } from '../entities'

export const SidebarElement: React.FC<{ course: Course }> = ({ course }) => {
  const [isOpenDeleteCourseAlert, setIsOpenDeleteCourseAlert] = useState(false);
  const [isOpenUpdateCourseModal, setIsOpenUpdateCourseModal] = useState(false);
  const navigate = useNavigate();
  return (
    <Flex justify="space-between">
      <Text onClick={() => navigate(`/course/${course.id}`)} fontWeight="semibold" cursor="pointer" _hover={{ color: 'teal' }}>{course.title}</Text>
      <Menu placement='bottom'>
        <MenuButton as={IconButton} icon={<RxDotsHorizontal />} size="sm" variant="ghost">
          Actions
        </MenuButton>
        <MenuList py="0" borderRadius="12px" overflow="hidden" >
          <MenuItem onClick={() => setIsOpenUpdateCourseModal(true)}>
            <Flex align="center" gap="12px">
              <FiEdit3 />
              <Text fontWeight="500">Edit</Text>
            </Flex>
          </MenuItem>
          <MenuItem onClick={() => setIsOpenDeleteCourseAlert(true)}>
            <Flex align="center" gap="12px">
              <FiTrash2 color="red" />
              <Text color="red" fontWeight="500">Delete</Text>
            </Flex>
          </MenuItem>
        </MenuList>
      </Menu>
      <UpdateCourseModal course={course} isOpen={isOpenUpdateCourseModal} onClose={() => { setIsOpenUpdateCourseModal(false) }} />
      <DeleteCourseAlert course={course} isOpen={isOpenDeleteCourseAlert} onClose={() => { setIsOpenDeleteCourseAlert(false) }} />
    </Flex>
  )
}

