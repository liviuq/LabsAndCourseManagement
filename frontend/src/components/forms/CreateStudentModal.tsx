import { Modal, ModalOverlay, ModalContent, ModalHeader, ModalCloseButton, ModalBody, Flex, FormControl, FormLabel, Input, NumberInput, NumberInputField, ModalFooter, Button } from '@chakra-ui/react'
import React, { useState } from 'react'
import { useCreateStudent } from '../../cache/create';
import { CreateStudent } from '../../entities/Create';
import { useToast } from '../../hooks';

interface CreateStudentModalProps {
  isOpen: boolean;
  setIsOpen: (isOpen: boolean) => void;
}

export const CreateStudentModal: React.FC<CreateStudentModalProps> = ({ isOpen, setIsOpen }) => {
  const toast = useToast();
  const [createStudentInput, setCreateStudentInput] = useState<CreateStudent>(CreateStudent.of({}));
  const createStudent = useCreateStudent(() => {
    toast({
      title: 'Student added successfuly.',
      status: 'success',
    })
    setCreateStudentInput(CreateStudent.of({}))
    setIsOpen(false);
  }, (e) =>
    toast({
      title: e,
      status: 'error',
    })
  );
  const handleSubmit = (e: any) => {
    e.preventDefault()
    createStudent.mutate(createStudentInput)
  }

  return (
    <Modal isCentered size="xl" isOpen={isOpen} onClose={() => setIsOpen(false)}>
      <ModalOverlay />
      <ModalContent>
        <ModalHeader>Add Student</ModalHeader>
        <ModalCloseButton />
        <form onSubmit={handleSubmit}>
          <ModalBody>
            <Flex direction="column" alignItems="center" px="32px" py="10px" gap="6px">
              <FormControl mb="6">
                <FormLabel m="0">First Name</FormLabel>
                <Input required onChange={(event) => {
                  setCreateStudentInput(prev => ({ ...prev, firstName: event.target.value }))
                }} fontSize="lg" textColor="#00ABB3" px="10px" variant='flushed' placeholder='First Name' _placeholder={{ color: "#00ABB3" }} />
              </FormControl>
              <FormControl mb="6">
                <FormLabel m="0">Last Name</FormLabel>
                <Input required onChange={(event) => {
                  setCreateStudentInput(prev => ({ ...prev, lastName: event.target.value }))
                }} fontSize="lg" textColor="#00ABB3" px="10px" variant='flushed' placeholder='Last Name' _placeholder={{ color: "#00ABB3" }} />
              </FormControl>
              <FormControl mb="6">
                <FormLabel m="0">Email</FormLabel>
                <Input type="email" required onChange={(event) => {
                  setCreateStudentInput(prev => ({ ...prev, email: event.target.value }))
                }} fontSize="lg" textColor="#00ABB3" px="10px" variant='flushed' placeholder='Email' _placeholder={{ color: "#00ABB3" }} />
              </FormControl>
              <FormControl mb="6">
                <FormLabel m="0">Semester</FormLabel>
                <NumberInput defaultValue={0} min={0} variant="flushed" >
                  <NumberInputField required onChange={(event) => {
                    setCreateStudentInput(prev => ({ ...prev, semester: parseInt(event.target.value) }))
                  }} fontSize="lg" textColor="#00ABB3" px="10px" placeholder='Semester' _placeholder={{ color: "#00ABB3" }} />
                </NumberInput>
              </FormControl>
              <FormControl mb="6">
                <FormLabel m="0">Group</FormLabel>
                <Input required onChange={(event) => {
                  setCreateStudentInput(prev => ({ ...prev, group: event.target.value }))
                }} fontSize="lg" textColor="#00ABB3" px="10px" variant='flushed' placeholder='Group' _placeholder={{ color: "#00ABB3" }} />
              </FormControl>
              <FormControl mb="6">
                <FormLabel m="0">Scholarship</FormLabel>
                <NumberInput defaultValue={0} min={0} variant="flushed" >
                  <NumberInputField required onChange={(event) => {
                    setCreateStudentInput(prev => ({ ...prev, scholarship: parseInt(event.target.value) }))
                  }} fontSize="lg" textColor="#00ABB3" px="10px" placeholder='Scholarship' _placeholder={{ color: "#00ABB3" }} />
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

