import { Modal, ModalOverlay, ModalContent, ModalHeader, ModalCloseButton, ModalBody, Flex, FormControl, FormLabel, Input, NumberInput, NumberInputField, ModalFooter, Button } from '@chakra-ui/react'
import Axios from 'axios';
import React, { useState } from 'react'
import { useCreateCourse } from '../../cache/create';
import { CreateCourse } from '../../entities/Create';
import { useToast } from '../../hooks';

interface CreateCourseModalProps {
  isOpen: boolean;
  setIsOpen: (isOpen: boolean) => void;
}

export const CreateCourseModal: React.FC<CreateCourseModalProps> = ({ isOpen, setIsOpen }) => {
  const toast = useToast();
  const [createCourseInput, setCreateCourseInput] = useState<CreateCourse>(CreateCourse.of({}));
  const createCourse = useCreateCourse(() => {
    toast({
      title: 'Course added successfuly.',
      status: 'success',
    })
    setCreateCourseInput(CreateCourse.of({}))
    setIsOpen(false);
  }, (e) =>
    toast({
      title: e,
      status: 'error',
    })
  );
  const handleSubmit = (e: any) => {
    e.preventDefault()
    createCourse.mutate(createCourseInput)
  }

  return (
    <Modal isCentered size="xl" isOpen={isOpen} onClose={() => setIsOpen(false)}>
      <ModalOverlay />
      <ModalContent>
        <ModalHeader>Add course</ModalHeader>
        <ModalCloseButton />
        <form onSubmit={handleSubmit}>
          <ModalBody>
            <Flex direction="column" alignItems="center" px="32px" py="10px" gap="6px">
              <FormControl mb="6">
                <FormLabel m="0">Title</FormLabel>
                <Input required onChange={(event) => {
                  setCreateCourseInput(prev => ({ ...prev, title: event.target.value }))
                }} fontSize="lg" textColor="#00ABB3" px="10px" variant='flushed' placeholder='Title' _placeholder={{ color: "#00ABB3" }} />
              </FormControl>
              <FormControl mb="6">
                <FormLabel m="0">Semester</FormLabel>
                <NumberInput defaultValue={0} min={0} variant="flushed" >
                  <NumberInputField required onChange={(event) => {
                    setCreateCourseInput(prev => ({ ...prev, semester: parseInt(event.target.value) }))
                  }} fontSize="lg" textColor="#00ABB3" px="10px" placeholder='Semester' _placeholder={{ color: "#00ABB3" }} />
                </NumberInput>
              </FormControl>
              <FormControl mb="6">
                <FormLabel m="0">Credits</FormLabel>
                <NumberInput defaultValue={0} min={0} variant="flushed" >
                  <NumberInputField required onChange={(event) => {
                    setCreateCourseInput(prev => ({ ...prev, credits: parseInt(event.target.value) }))
                  }} fontSize="lg" textColor="#00ABB3" px="10px" placeholder='Credits' _placeholder={{ color: "#00ABB3" }} />
                </NumberInput>
              </FormControl>

            </Flex>
          </ModalBody>

          <ModalFooter >
            <Button onClick={() => setIsOpen(false)} size="md" variant="solid" mr="10px">Cancel</Button>
            <Button type="submit" size="md" variant="solid" colorScheme="teal">Submit</Button>
          </ModalFooter>
        </form>

      </ModalContent>
    </Modal >
  )
}

