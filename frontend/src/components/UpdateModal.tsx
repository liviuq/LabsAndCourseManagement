import { Modal, ModalOverlay, ModalContent, ModalHeader, ModalCloseButton, ModalBody, ModalFooter, Button, Text, Flex, Box, FormControl, FormLabel, Input, NumberInput, NumberInputField } from '@chakra-ui/react'
import axios from 'axios';
import React, { useEffect, useState } from 'react'
import { Student } from '../entities/Student';
import { Teacher } from '../entities/Teacher';

interface UpdateModalProps {
  isOpen: boolean;
  setIsOpen: (isOpen: boolean) => void;
  entity: Student | Teacher;
}

export const UpdateModal: React.FC<UpdateModalProps> = ({ isOpen, setIsOpen, entity }) => {
  const [entityUpdateInput, setEntityUpdateInput] = useState(
    {
      ...entity
    }
  );
  const [isTeacher, setIsTeacher] = useState(false);
  const handleUpdate = () => {
    if (isTeacher)
      axios.put(`https://localhost:7202/api/Teachers/${entity.id}`, entityUpdateInput);
    else
      axios.put(`https://localhost:7202/api/Students/${entity.id}`, entityUpdateInput);
    window.location.reload();
  }
  useEffect(() => {
    setEntityUpdateInput({
      ...entity
    })
    setIsTeacher('teachingDegree' in entity)
    console.log(isTeacher)
  }, [isOpen])

  return (
    <Modal size="2xl" isOpen={isOpen} onClose={() => setIsOpen(false)}>
      <ModalOverlay />
      <ModalContent>
        <ModalHeader>Update Student</ModalHeader>
        <ModalCloseButton />
        <form onSubmit={handleUpdate}>
          <ModalBody>
            <Flex direction="column" alignItems="center" p="32px" gap="6px">
              <FormControl mb="6">
                <FormLabel m="0">First Name</FormLabel>
                <Input defaultValue={entity.firstName} required onChange={(event) => {
                  setEntityUpdateInput(entityUpdateInput => ({ ...entityUpdateInput, ...{ firstName: event.target.value } }))
                }} fontSize="lg" textColor="#00ABB3" px="10px" variant='flushed' placeholder='First Name' _placeholder={{ color: "#00ABB3" }} />
              </FormControl>
              <FormControl mb="6">
                <FormLabel m="0">Last Name</FormLabel>
                <Input defaultValue={entity.lastName} required onChange={(event) => {
                  setEntityUpdateInput(entityUpdateInput => ({ ...entityUpdateInput, ...{ lastName: event.target.value } }))
                }} fontSize="lg" textColor="#00ABB3" px="10px" variant='flushed' placeholder='Last Name' _placeholder={{ color: "#00ABB3" }} />
              </FormControl>
              <FormControl mb="6">
                <FormLabel m="0">Email</FormLabel>
                <Input defaultValue={entity.email} type="email" required onChange={(event) => {
                  setEntityUpdateInput(entityUpdateInput => ({ ...entityUpdateInput, ...{ email: event.target.value } }))
                }} fontSize="lg" textColor="#00ABB3" px="10px" variant='flushed' placeholder='Email' _placeholder={{ color: "#00ABB3" }} />
              </FormControl>
              {
                isTeacher ?
                  <FormControl mb="6">
                    <FormLabel m="0">Teaching Degree</FormLabel>
                    <Input defaultValue={entity.teachingDegree} required onChange={(event) => {
                      setEntityUpdateInput(entityUpdateInput => ({ ...entityUpdateInput, ...{ teachingDegree: event.target.value } }))
                    }} fontSize="lg" textColor="#00ABB3" px="10px" variant='flushed' placeholder='Teaching Degree' _placeholder={{ color: "#00ABB3" }} />
                  </FormControl>
                  :
                  <>
                    <FormControl mb="6">
                      <FormLabel m="0">Semester</FormLabel>
                      <NumberInput defaultValue={entity.semester} min={0} variant="flushed" >
                        <NumberInputField required onChange={(event) => {
                          setEntityUpdateInput(entityUpdateInput => ({ ...entityUpdateInput, ...{ semester: parseInt(event.target.value) } }))
                        }} fontSize="lg" textColor="#00ABB3" px="10px" placeholder='Semester' _placeholder={{ color: "#00ABB3" }} />
                      </NumberInput>
                    </FormControl>
                    <FormControl mb="6">
                      <FormLabel m="0">Group</FormLabel>
                      <Input defaultValue={entity.group} required onChange={(event) => {
                        setEntityUpdateInput(entityUpdateInput => ({ ...entityUpdateInput, ...{ group: event.target.value } }))
                      }} fontSize="lg" textColor="#00ABB3" px="10px" variant='flushed' placeholder='Group' _placeholder={{ color: "#00ABB3" }} />
                    </FormControl>
                    <FormControl mb="6">
                      <FormLabel m="0">Scholarship</FormLabel>
                      <NumberInput defaultValue={entity.scholarship} min={0} variant="flushed" >
                        <NumberInputField required onChange={(event) => {
                          setEntityUpdateInput(entityUpdateInput => ({ ...entityUpdateInput, ...{ scholarship: parseInt(event.target.value) } }))
                        }} fontSize="lg" textColor="#00ABB3" px="10px" placeholder='Scholarship' _placeholder={{ color: "#00ABB3" }} />
                      </NumberInput>
                    </FormControl>
                  </>
              }
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

