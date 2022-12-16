import { Button, Flex, Text } from "@chakra-ui/react";
import { useNavigate } from "react-router-dom";
import React from "react";

export const Sidebar: React.FC = () => {
  const navigate = useNavigate();
  return (
    <Flex h="94vh" w="350px" bg="#EAEAEA" direction="column">
      <Button
        onClick={() => navigate("/teacher")}
        variant="solid"
        colorScheme="teal"
        borderRadius="0"
      >
        Teachers
      </Button>
      <Button
        onClick={() => navigate("/student")}
        variant="solid"
        colorScheme="teal"
        borderRadius="0"
      >
        Students
      </Button>
    </Flex>
  );
};
