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

export const InfoModal: React.FC<UpdateModalProps> = ({ isOpen, setIsOpen, entity }) => {

  return (
    <Modal size="2xl" isOpen={isOpen} onClose={() => setIsOpen(false)}>
      <ModalOverlay />
      <ModalContent>
        <ModalHeader alignSelf="center">Additional Information</ModalHeader>
        <ModalCloseButton />
        <ModalBody>
          <Flex gap="16px">
            <Flex direction="column" alignItems="flex-end" gap="6px">
              {Object.entries(entity).map(([key, value]) => {
                return (
                  <Text fontWeight="semibold">{key}:</Text>
                )
              })}
            </Flex>
            <Flex direction="column" alignItems="flex-start" gap="6px">
              {Object.entries(entity).map(([key, value]) => {
                return (
                  <Text>{value}</Text>
                )
              })}
            </Flex>
          </Flex>
        </ModalBody>
        <ModalFooter >
          <Button onClick={() => setIsOpen(false)} size="md" variant="ghost" colorScheme="gray">Close</Button>
        </ModalFooter>
      </ModalContent>
    </Modal >
  )
}

