import { Box, Button, Flex, FormControl, FormLabel, Heading, Image, Input, NumberInput, NumberInputField } from '@chakra-ui/react'
import React, { useState } from 'react'
import { Header } from '../../shared'
import createCourseImg from "../../assets/createCourse.svg";
import { useToast } from '../../hooks';
import { CreateCourse } from '../../entities/Create';
import { useCreateCourse } from '../../cache/create';
import { Course } from '../../entities';

interface TutorialCreateCourseProps {
  nextStep: () => void,
  setCourseId: (courseId: string) => void,
}

export const TutorialCreateCourse: React.FC<TutorialCreateCourseProps> = ({ nextStep, setCourseId }) => {
  const toast = useToast();
  const [createCourseInput, setCreateCourseInput] = useState<CreateCourse>(CreateCourse.of({}));
  const createCourse = useCreateCourse((course: Course) => {
    setCourseId(course.id)
    toast({
      title: 'Course created successfuly.',
      status: 'success',
    })
    nextStep()
  }, (e) => toast({
    title: e,
    status: 'error',
  }));

  const handleSubmit = (e: any) => {
    e.preventDefault()
    createCourse.mutate(createCourseInput)
  }
  return (
    <>
      <Header />
      <Flex w="full" h="full">
        <Flex w="full" direction="column" align="center" justifyContent="center" mb="200px">
          <Heading fontSize="34px" textAlign="center" color="#00ABB3" mb="48px">Sign up your course</Heading>
          <Image boxSize="200px" src={createCourseImg} alt="" />
          <form onSubmit={handleSubmit}>
            <Flex direction="column" alignItems="center" p="32px" gap="6px">
              <FormControl mb="6">
                <FormLabel m="0">Title</FormLabel>
                <Input onChange={(e) => {
                  setCreateCourseInput(prev => ({ ...prev, title: e.target.value }))
                }} required fontSize="lg" textColor="#00ABB3" px="10px" variant='flushed' placeholder='Title' _placeholder={{ color: "#00ABB3" }} />
              </FormControl>
              <FormControl mb="6">
                <FormLabel m="0">Semester</FormLabel>
                <NumberInput min={0} variant="flushed" >
                  <NumberInputField required onChange={(e) => {
                    setCreateCourseInput(prev => ({ ...prev, semester: parseInt(e.target.value) }))
                  }} fontSize="lg" textColor="#00ABB3" px="10px" placeholder='Semester' _placeholder={{ color: "#00ABB3" }} />
                </NumberInput>
              </FormControl>
              <FormControl mb="6">
                <FormLabel m="0">Credits</FormLabel>
                <NumberInput min={0} variant="flushed" >
                  <NumberInputField required onChange={(e) => {
                    setCreateCourseInput(prev => ({ ...prev, credits: parseInt(e.target.value) }))
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

