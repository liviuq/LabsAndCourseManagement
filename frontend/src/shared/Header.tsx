import React, { useState } from "react";
import {
  Text,
  Avatar,
  Button,
  Flex,
  Menu,
  MenuButton,
  MenuItem,
  MenuList,
} from "@chakra-ui/react";
import { GiBookmarklet } from "react-icons/gi";
import { LangChanger } from "./LangChanger";
import { useNavigate } from "react-router-dom";
import { FiChevronDown, FiLogOut, FiUserPlus } from "react-icons/fi";
import { CreateStudentModal, CreateTeacherModal } from "../components/forms";

export const Header: React.FC = () => {
  const navigate = useNavigate();
  const [isOpenCreateTeacherModal, setIsOpenCreateTeacherModal] = useState<boolean>(false)
  const [isOpenCreateStudentModal, setIsOpenCreateStudentModal] = useState<boolean>(false)
  return (
    <Flex
      h="6vh"
      w="full"
      align="center"
      px="32px"
      justify="space-between"
      bg="white"
      shadow="0px 2px 8px rgba(0, 0, 0, 0.2)"
      zIndex="10"
    >
      <GiBookmarklet
        onClick={() => navigate("/")}
        size="4vh"
        color="#00ABB3"
        cursor="pointer"
      />
      <Flex align="center" gap="32px">
        <Flex align="center" gap="12px">
          <Menu placement="bottom-end">
            <MenuButton as={Button} rightIcon={<FiChevronDown />} size="sm" alignItems="center">
              achimstefan@yahoo.com
            </MenuButton>
            <MenuList py="0px" borderRadius="12px" overflow="hidden">
              <MenuItem onClick={() => setIsOpenCreateTeacherModal(true)}>
                <Flex align="center" gap="12px">
                  <FiUserPlus />
                  <Text>Add Teacher</Text>
                </Flex>
              </MenuItem>
              <MenuItem onClick={() => setIsOpenCreateStudentModal(true)}>
                <Flex align="center" gap="12px">
                  <FiUserPlus />
                  <Text>Add Student</Text>
                </Flex>
              </MenuItem>
              <MenuItem>
                <Flex align="center" gap="12px">
                  <FiLogOut />
                  <Text>Logout</Text>
                </Flex>
              </MenuItem>
            </MenuList>
          </Menu>
          <Avatar boxSize="32px" bg="#00ABB3" />
        </Flex>
        <LangChanger />
      </Flex>
      <CreateStudentModal isOpen={isOpenCreateStudentModal} setIsOpen={setIsOpenCreateStudentModal} />
      <CreateTeacherModal isOpen={isOpenCreateTeacherModal} setIsOpen={setIsOpenCreateTeacherModal} />
    </Flex>
  );
};
