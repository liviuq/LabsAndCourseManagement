import { Box, Button, Flex, FormControl, FormLabel, Heading, Image, Input, NumberInput, NumberInputField } from '@chakra-ui/react'
import React, { useEffect, useState } from 'react'
import { Header } from '../shared'
import createCourseImg from "../assets/createCourse.svg";
import { Course } from '../entities/Course';
import Axios from "axios";
import { generatePath, useNavigate } from 'react-router-dom';


export const CreateCourse: React.FC = () => {
  const navigate = useNavigate();
  const [courseCreateInput, setCourseCreateInput] = useState<Course>(
    {
      title: '',
      semester: 0,
      credits: 0,
    }
  );

  const handleSubmit = (e: any) => {
    e.preventDefault()
    Axios({
      method: "POST",
      url: "https://localhost:7202/api/courses",
      data: courseCreateInput,
    });
    navigate(`/build-team`)
  }
  return (
    <>
      <Header />
      <Flex w="full" h="full">
        <Flex w="full" direction="column" align="center" justifyContent="center" mb="200px">
          <Heading fontSize="34px" textAlign="center" color="#00ABB3" mb="48px">Sign up your first course</Heading>
          <Image boxSize="200px" src={createCourseImg} alt="" />
          <form onSubmit={handleSubmit}>
            <Flex direction="column" alignItems="center" p="32px" gap="6px">
              <FormControl mb="6">
                <FormLabel m="0">Title</FormLabel>
                <Input onChange={(event) => {
                  setCourseCreateInput(courseCreateInput => ({ ...courseCreateInput, ...{ title: event.target.value } }))
                }} required fontSize="lg" textColor="#00ABB3" px="10px" variant='flushed' placeholder='Title' _placeholder={{ color: "#00ABB3" }} />
              </FormControl>
              <FormControl mb="6">
                <FormLabel m="0">Semester</FormLabel>
                <NumberInput min={0} variant="flushed" >
                  <NumberInputField required onChange={(event) => {
                    setCourseCreateInput(courseCreateInput => ({ ...courseCreateInput, ...{ semester: parseInt(event.target.value) } }))
                  }} fontSize="lg" textColor="#00ABB3" px="10px" placeholder='Semester' _placeholder={{ color: "#00ABB3" }} />
                </NumberInput>
              </FormControl>
              <FormControl mb="6">
                <FormLabel m="0">Credits</FormLabel>
                <NumberInput min={0} variant="flushed" >
                  <NumberInputField required onChange={(event) => {
                    setCourseCreateInput(courseCreateInput => ({ ...courseCreateInput, ...{ credits: parseInt(event.target.value) } }))
                  }} fontSize="lg" textColor="#00ABB3" px="10px" placeholder='Credits' _placeholder={{ color: "#00ABB3" }} />
                </NumberInput>
              </FormControl>
              <Box >
                <Button type="submit" size="lg" variant="solid" colorScheme="teal">Submit</Button>
              </Box>
            </Flex>
          </form>
        </Flex>

      </Flex>
    </>
  )
}

