import { Avatar, Button, Flex, IconButton, Menu, MenuButton, MenuItem, MenuList, Text } from '@chakra-ui/react'
import React from 'react'
import { FiX } from 'react-icons/fi'
import { RxUpdate } from 'react-icons/rx'
import { TbDotsVertical } from 'react-icons/tb'
import { Student } from '../entities/Student'
import Axios from "axios";
import { Teacher } from '../entities/Teacher'

interface EntityCardProps {
  setUpdateOpen: (isOpen: boolean) => void;
  setInfoOpen: (isOpen: boolean) => void;
  setSelectedEntity: (selectedEntity: any) => void;
  entity: Student | Teacher;
}

export const EntityCard: React.FC<EntityCardProps> = ({ entity, setUpdateOpen, setInfoOpen, setSelectedEntity }) => {
  const handleDelete = () => {
    if ('teachingDegree' in entity)
      Axios.delete(`https://localhost:7202/api/Teachers/${entity.id}`);
    else
      Axios.delete(`https://localhost:7202/api/Students/${entity.id}`);
    window.location.reload();
  }
  return (
    <Flex w="full" border="1px solid #D0D5DD" align="center" bg="white" borderRadius="12px" p="16px" boxShadow="0px 1px 2px rgba(16, 24, 40, 0.05)" justify="space-between">
      <Flex gap="12px" align="center">
        <Avatar boxSize="40px" />
        <Flex direction="column" gap="0" >
          <Text fontSize="16px" fontWeight="bold">{entity.firstName + " " + entity.lastName}</Text>
          <Text fontSize="12px">{entity.email}</Text>
        </Flex>
      </Flex>
      <Flex gap="12px">
        <Button onClick={() => { setSelectedEntity(entity); setUpdateOpen(true); }} size="sm" variant="ghost" colorScheme="gray" leftIcon={<RxUpdate size="14px" />} iconSpacing="4px">Update</Button>
        <Button onClick={handleDelete} size="sm" variant="ghost" colorScheme="red" leftIcon={<FiX size="14px" />} iconSpacing="4px">Delete</Button>
        <Menu placement='bottom-end' >
          <MenuButton as={IconButton} variant="ghost" size="sm" icon={<TbDotsVertical size="18px" />} />
          <MenuList py="0">
            <MenuItem onClick={() => { setSelectedEntity(entity); setInfoOpen(true); }}>More Info</MenuItem>
          </MenuList>
        </Menu>
      </Flex>
    </Flex>
  )
}

