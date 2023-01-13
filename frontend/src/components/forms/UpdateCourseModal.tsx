import { Modal, ModalOverlay, ModalContent, ModalHeader, ModalCloseButton, ModalBody, Flex, FormControl, FormLabel, Input, NumberInput, NumberInputField, ModalFooter, Button } from '@chakra-ui/react'
import Axios from 'axios';
import React, { useState } from 'react'
import { useCreateCourse } from '../../cache/create';
import { useUpdateCourse } from '../../cache/update';
import { Course } from '../../entities';
import { CreateCourse } from '../../entities/Create';
import { useToast } from '../../hooks';

interface UpdateCourseModalProps {
  course: Course;
  isOpen: boolean;
  onClose: () => void;
}

export const UpdateCourseModal: React.FC<UpdateCourseModalProps> = ({ course, isOpen, onClose }) => {
  const toast = useToast();
  const [updateCourseInput, setUpdateCourseInput] = useState<Course>(Course.of(course));
  const updateCourse = useUpdateCourse(
    course.id,
    () => {
      toast({
        title: 'Course updated successfuly.',
        status: 'success',
      })
      onClose()
    },
    (e) =>
      toast({
        title: e,
        status: 'error',
      })
  );
  const handleSubmit = (e: any) => {
    e.preventDefault()
    updateCourse.mutate(updateCourseInput)
  }

  return (
    <Modal isCentered size="xl" isOpen={isOpen} onClose={onClose}>
      <ModalOverlay />
      <ModalContent>
        <ModalHeader>Edit course</ModalHeader>
        <ModalCloseButton />
        <form onSubmit={handleSubmit}>
          <ModalBody>
            <Flex direction="column" alignItems="center" px="32px" py="10px" gap="6px">
              <FormControl mb="6">
                <FormLabel m="0">Title</FormLabel>
                <Input defaultValue={course.title} required onChange={(event) => {
                  setUpdateCourseInput(prev => ({ ...prev, title: event.target.value }))
                }} fontSize="lg" textColor="#00ABB3" px="10px" variant='flushed' placeholder='Title' _placeholder={{ color: "#00ABB3" }} />
              </FormControl>
              <FormControl mb="6">
                <FormLabel m="0">Semester</FormLabel>
                <NumberInput defaultValue={course.semester} min={0} variant="flushed" >
                  <NumberInputField required onChange={(event) => {
                    setUpdateCourseInput(prev => ({ ...prev, semester: parseInt(event.target.value) }))
                  }} fontSize="lg" textColor="#00ABB3" px="10px" placeholder='Semester' _placeholder={{ color: "#00ABB3" }} />
                </NumberInput>
              </FormControl>
              <FormControl mb="6">
                <FormLabel m="0">Credits</FormLabel>
                <NumberInput defaultValue={course.credits} min={0} variant="flushed" >
                  <NumberInputField required onChange={(event) => {
                    setUpdateCourseInput(prev => ({ ...prev, credits: parseInt(event.target.value) }))
                  }} fontSize="lg" textColor="#00ABB3" px="10px" placeholder='Credits' _placeholder={{ color: "#00ABB3" }} />
                </NumberInput>
              </FormControl>

            </Flex>
          </ModalBody>

          <ModalFooter >
            <Button onClick={onClose} size="md" variant="solid" mr="10px">Cancel</Button>
            <Button type="submit" size="md" variant="solid" colorScheme="teal">Submit</Button>
          </ModalFooter>
        </form>

      </ModalContent>
    </Modal >
  )
}

