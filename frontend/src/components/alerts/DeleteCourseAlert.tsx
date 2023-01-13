import { Text, AlertDialog, AlertDialogOverlay, AlertDialogContent, AlertDialogHeader, AlertDialogBody, Flex, AlertDialogFooter, Button } from '@chakra-ui/react'
import React, { useRef } from 'react'
import { RiErrorWarningLine } from 'react-icons/ri'
import { useQueryClient } from 'react-query';
import { useDeleteCourse } from '../../cache/delete';
import { Course } from '../../entities';
import { useToast } from '../../hooks';

interface DeleteCourseAlertProps {
  course: Course,
  isOpen: boolean;
  onClose: () => void;
}

export const DeleteCourseAlert: React.FC<DeleteCourseAlertProps> = ({ course, isOpen, onClose }) => {
  const cancelRef = useRef(null);
  const toast = useToast();
  const queryClient = useQueryClient();
  const onSubmit = () => {
    useDeleteCourse(
      course.id,
      () => {
        toast({
          title: 'Course deleted successfuly.',
          status: 'success',
        })
        queryClient.invalidateQueries('getCourses')
        onClose()
      },
      (e) => {
        toast({
          title: e,
          status: 'error',
        })
      })
  }
  return (
    <AlertDialog
      isCentered
      isOpen={isOpen}
      leastDestructiveRef={cancelRef}
      onClose={onClose}
    >
      <AlertDialogOverlay>
        <AlertDialogContent>
          <AlertDialogHeader fontSize='lg' fontWeight='bold'>
            <Text>
              Delete <Text as="span" color="teal">{' ' + course.title}</Text>
            </Text>
          </AlertDialogHeader>

          <AlertDialogBody mb="25px">
            <Flex direction="column" w="full" align="center" gap="12px">
              <RiErrorWarningLine size="48px" color="Red" />
              <Text textAlign="center" fontWeight="bold" fontSize="24px">
                Are you sure?
              </Text>
              <Text textAlign="center" color="red">
                You can't undo this action afterwards.
              </Text>
            </Flex>
          </AlertDialogBody>

          <AlertDialogFooter>
            <Button ref={cancelRef} onClick={onClose}>
              Cancel
            </Button>
            <Button colorScheme='red' onClick={onSubmit} ml={3}>
              Delete
            </Button>
          </AlertDialogFooter>
        </AlertDialogContent>
      </AlertDialogOverlay>
    </AlertDialog>
  )
}

