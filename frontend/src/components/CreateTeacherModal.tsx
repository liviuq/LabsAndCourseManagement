import { Modal, ModalOverlay, ModalContent, ModalHeader, ModalCloseButton, ModalBody, Flex, FormControl, FormLabel, Input, NumberInput, NumberInputField, ModalFooter, Button } from '@chakra-ui/react'
import Axios from 'axios';
import React, { useState } from 'react'
import { Teacher } from '../entities/Teacher';

interface CreateTeacherModalProps {
  isOpen: boolean;
  setIsOpen: (isOpen: boolean) => void;
}

export const CreateTeacherModal: React.FC<CreateTeacherModalProps> = ({ isOpen, setIsOpen }) => {
  const teacherEmptyState = {
    firstName: '',
    lastName: '',
    email: '',
    teachingDegree: ''
  }
  const [teacherInput, setTeacherInput] = useState<Teacher>(
    teacherEmptyState
  );
  const handleSubmit = (e: any) => {
    e.preventDefault()
    Axios({
      method: "POST",
      url: "https://localhost:7202/api/teachers",
      data: teacherInput,
    });
    setIsOpen(false);
    setTeacherInput(
      teacherEmptyState
    );
  }

  return (
    <Modal size="2xl" isOpen={isOpen} onClose={() => setIsOpen(false)}>
      <ModalOverlay />
      <ModalContent>
        <ModalHeader>Add teacher</ModalHeader>
        <ModalCloseButton />
        <form onSubmit={handleSubmit}>
          <ModalBody>
            <Flex direction="column" alignItems="center" p="32px" gap="6px">
              <FormControl mb="6">
                <FormLabel m="0">First Name</FormLabel>
                <Input required onChange={(event) => {
                  setTeacherInput(teacherInput => ({ ...teacherInput, ...{ firstName: event.target.value } }))
                }} fontSize="lg" textColor="#00ABB3" px="10px" variant='flushed' placeholder='First Name' _placeholder={{ color: "#00ABB3" }} />
              </FormControl>
              <FormControl mb="6">
                <FormLabel m="0">Last Name</FormLabel>
                <Input required onChange={(event) => {
                  setTeacherInput(teacherInput => ({ ...teacherInput, ...{ lastName: event.target.value } }))
                }} fontSize="lg" textColor="#00ABB3" px="10px" variant='flushed' placeholder='Last Name' _placeholder={{ color: "#00ABB3" }} />
              </FormControl>
              <FormControl mb="6">
                <FormLabel m="0">Email</FormLabel>
                <Input type="email" required onChange={(event) => {
                  setTeacherInput(teacherInput => ({ ...teacherInput, ...{ email: event.target.value } }))
                }} fontSize="lg" textColor="#00ABB3" px="10px" variant='flushed' placeholder='Email' _placeholder={{ color: "#00ABB3" }} />
              </FormControl>
              <FormControl mb="6">
                <FormLabel m="0">Teaching Degree</FormLabel>
                <Input required onChange={(event) => {
                  setTeacherInput(teacherInput => ({ ...teacherInput, ...{ teachingDegree: event.target.value } }))
                }} fontSize="lg" textColor="#00ABB3" px="10px" variant='flushed' placeholder='Teaching Degree' _placeholder={{ color: "#00ABB3" }} />
              </FormControl>
            </Flex>
          </ModalBody>

          <ModalFooter >
            <Button type="submit" size="md" variant="solid" colorScheme="teal">Submit</Button>
          </ModalFooter>
        </form>

      </ModalContent>
    </Modal >
  )
}

