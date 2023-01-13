import { Modal, ModalOverlay, ModalContent, ModalHeader, ModalCloseButton, ModalBody, Flex, FormControl, FormLabel, Input, NumberInput, NumberInputField, ModalFooter, Button } from '@chakra-ui/react'
import React, { useState } from 'react'
import { useCreateTeacher } from '../../cache/create';
import { CreateTeacher } from '../../entities/Create';
import { useToast } from '../../hooks';

interface CreateTeacherModalProps {
  isOpen: boolean;
  setIsOpen: (isOpen: boolean) => void;
}

export const CreateTeacherModal: React.FC<CreateTeacherModalProps> = ({ isOpen, setIsOpen }) => {
  const toast = useToast();
  const [createTeacherInput, setCreateTeacherInput] = useState<CreateTeacher>(CreateTeacher.of({}));
  const createTeacher = useCreateTeacher(() => {
    toast({
      title: 'Teacher added successfuly.',
      status: 'success',
    })
    setCreateTeacherInput(CreateTeacher.of({}))
    setIsOpen(false);
  }, (e) =>
    toast({
      title: e,
      status: 'error',
    })
  );
  const handleSubmit = (e: any) => {
    e.preventDefault()
    createTeacher.mutate(createTeacherInput)
  }

  return (
    <Modal isCentered size="xl" isOpen={isOpen} onClose={() => setIsOpen(false)}>
      <ModalOverlay />
      <ModalContent>
        <ModalHeader>Add teacher</ModalHeader>
        <ModalCloseButton />
        <form onSubmit={handleSubmit}>
          <ModalBody>
            <Flex direction="column" alignItems="center" px="32px" py="10px" gap="6px">
              <FormControl mb="6">
                <FormLabel m="0">First Name</FormLabel>
                <Input required onChange={(event) => {
                  setCreateTeacherInput(prev => ({ ...prev, firstName: event.target.value }))
                }} fontSize="lg" textColor="#00ABB3" px="10px" variant='flushed' placeholder='First Name' _placeholder={{ color: "#00ABB3" }} />
              </FormControl>
              <FormControl mb="6">
                <FormLabel m="0">Last Name</FormLabel>
                <Input required onChange={(event) => {
                  setCreateTeacherInput(prev => ({ ...prev, lastName: event.target.value }))
                }} fontSize="lg" textColor="#00ABB3" px="10px" variant='flushed' placeholder='Last Name' _placeholder={{ color: "#00ABB3" }} />
              </FormControl>
              <FormControl mb="6">
                <FormLabel m="0">Email</FormLabel>
                <Input type="email" required onChange={(event) => {
                  setCreateTeacherInput(prev => ({ ...prev, email: event.target.value }))
                }} fontSize="lg" textColor="#00ABB3" px="10px" variant='flushed' placeholder='Email' _placeholder={{ color: "#00ABB3" }} />
              </FormControl>
              <FormControl mb="6">
                <FormLabel m="0">Teaching Degree</FormLabel>
                <Input required onChange={(event) => {
                  setCreateTeacherInput(prev => ({ ...prev, teachingDegree: event.target.value }))
                }} fontSize="lg" textColor="#00ABB3" px="10px" variant='flushed' placeholder='Teaching Degree' _placeholder={{ color: "#00ABB3" }} />
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

