import { Box, Button, Flex, Heading, Image } from '@chakra-ui/react'
import React from 'react'
import { Header } from '../shared'
import welcomeImg from "../assets/welcome.svg";
import { ImArrowDownLeft } from 'react-icons/im'
import { useNavigate } from 'react-router-dom';
export const Home: React.FC = () => {
  const navigate = useNavigate();
  return (
    <>
      <Header />
      <Flex w="full" h="full">
        <Flex w="full" direction="column" align="center" justifyContent="center" mb="200px">
          <Heading fontSize="82px" textAlign="center" color="#00ABB3" mb="48px">Course Manager</Heading>
          <Box position="relative">
            <Image src={welcomeImg} alt="" />
            <Box position="absolute" right="150px" top="50px">
              <ImArrowDownLeft size="64px" color="#00ABB3" />
            </Box>
            <Button onClick={() => navigate("/create-course")} position="absolute" right="190px" top="150px" variant="solid" colorScheme="teal">Get Started</Button>
          </Box>
        </Flex>
      </Flex>
    </>

  )
}

