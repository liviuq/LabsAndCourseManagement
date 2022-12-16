import React from "react";
import {
  Avatar,
  Button,
  Flex,
  Menu,
  MenuButton,
  MenuItem,
  MenuList,
  Text,
} from "@chakra-ui/react";
import { GiBookmarklet } from "react-icons/gi";
import { LangChanger } from "./LangChanger";
import { useNavigate } from "react-router-dom";

export const Header: React.FC = () => {
  const navigate = useNavigate();
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
        onClick={() => navigate("/home")}
        size="4vh"
        color="#00ABB3"
        cursor="pointer"
      />
      <Flex align="center" gap="32px">
        {/* <Flex align="center" gap="12px">
          <Text fontSize="lg">Administrator</Text>
          <Avatar boxSize="32px" bg="#00ABB3" />
        </Flex> */}
        <LangChanger />
      </Flex>
    </Flex>
  );
};
