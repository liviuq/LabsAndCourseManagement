import React, { useEffect, useState } from 'react'
import {
  Box, Button, Flex, FormControl, FormLabel, Input,
  Accordion,
  AccordionItem,
  AccordionButton,
  AccordionPanel,
  AccordionIcon,
  Heading,
  NumberInput,
  NumberInputField,

} from '@chakra-ui/react'
import Axios from "axios";
import { EntityCard } from '../components'
import { Student } from '../entities/Student';
import { UpdateModal } from '../components/UpdateModal';
import { InfoModal } from '../components/InfoModal';
export const StudentCRUD: React.FC = () => {
  const [openUpdateModal, setOpenUpdateModal] = useState(false);
  const [openInfoModal, setOpenInfoModal] = useState(false);
  const [students, setStudents] = useState([]);
  const [selectedStudent, setSelectedStudent] = useState({
    firstName: '',
    lastName: '',
    email: '',
    semester: 0,
    group: '',
    scholarship: 0
  });
  const [studentCreateInput, setStudentCreateInput] = useState<Student>(
    {
      firstName: '',
      lastName: '',
      email: '',
      semester: 0,
      group: '',
      scholarship: 0
    }
  );
  
  useEffect(() => {
    console.log(studentCreateInput)
  }, [studentCreateInput])
  useEffect(() => {
    fetchStudents();
  }, [])
  const fetchStudents = () => {
    Axios.get("https://localhost:7202/api/Students").then((res) =>
      setStudents(res.data)
    )
  }
  const handleSubmit = (e: any) => {
    Axios({
      method: "POST",
      url: "https://localhost:7202/api/Students",
      data: studentCreateInput,
    });
  }
  return (
    <Flex w="full" direction="column" align="center">
      <Accordion allowMultiple w="500px" my="32px">
        <AccordionItem>
          <h2>
            <AccordionButton  >
              <Box flex='1' textAlign='left' >
                <Heading size="md" color="#00ABB3">Create</Heading>
              </Box>
              <AccordionIcon />
            </AccordionButton>
          </h2>
          <AccordionPanel pb={4}>
            <form onSubmit={handleSubmit}>
              <Flex direction="column" alignItems="center" p="32px" gap="6px">
                <FormControl mb="6">
                  <FormLabel m="0">First Name</FormLabel>
                  <Input required onChange={(event) => {
                    setStudentCreateInput(studentCreateInput => ({ ...studentCreateInput, ...{ firstName: event.target.value } }))
                  }} fontSize="lg" textColor="#00ABB3" px="10px" variant='flushed' placeholder='First Name' _placeholder={{ color: "#00ABB3" }} />
                </FormControl>
                <FormControl mb="6">
                  <FormLabel m="0">Last Name</FormLabel>
                  <Input required onChange={(event) => {
                    setStudentCreateInput(studentCreateInput => ({ ...studentCreateInput, ...{ lastName: event.target.value } }))
                  }} fontSize="lg" textColor="#00ABB3" px="10px" variant='flushed' placeholder='Last Name' _placeholder={{ color: "#00ABB3" }} />
                </FormControl>
                <FormControl mb="6">
                  <FormLabel m="0">Email</FormLabel>
                  <Input type="email" required onChange={(event) => {
                    setStudentCreateInput(studentCreateInput => ({ ...studentCreateInput, ...{ email: event.target.value } }))
                  }} fontSize="lg" textColor="#00ABB3" px="10px" variant='flushed' placeholder='Email' _placeholder={{ color: "#00ABB3" }} />
                </FormControl>
                <FormControl mb="6">
                  <FormLabel m="0">Semester</FormLabel>
                  <NumberInput min={0} variant="flushed" >
                    <NumberInputField required onChange={(event) => {
                      setStudentCreateInput(studentCreateInput => ({ ...studentCreateInput, ...{ semester: parseInt(event.target.value) } }))
                    }} fontSize="lg" textColor="#00ABB3" px="10px" placeholder='Semester' _placeholder={{ color: "#00ABB3" }} />
                  </NumberInput>
                </FormControl>
                <FormControl mb="6">
                  <FormLabel m="0">Group</FormLabel>
                  <Input required onChange={(event) => {
                    setStudentCreateInput(studentCreateInput => ({ ...studentCreateInput, ...{ group: event.target.value } }))
                  }} fontSize="lg" textColor="#00ABB3" px="10px" variant='flushed' placeholder='Group' _placeholder={{ color: "#00ABB3" }} />
                </FormControl>
                <FormControl mb="6">
                  <FormLabel m="0">Scholarship</FormLabel>
                  <NumberInput min={0} variant="flushed" >
                    <NumberInputField required onChange={(event) => {
                      setStudentCreateInput(studentCreateInput => ({ ...studentCreateInput, ...{ scholarship: parseInt(event.target.value) } }))
                    }} fontSize="lg" textColor="#00ABB3" px="10px" placeholder='Scholarship' _placeholder={{ color: "#00ABB3" }} />
                  </NumberInput>
                </FormControl>
                <Box >
                  <Button type="submit" size="lg" variant="solid" colorScheme="teal">Submit</Button>
                </Box>
              </Flex>
            </form>
          </AccordionPanel>
        </AccordionItem>
      </Accordion>
      <Flex w="800px" direction="column" gap="12px">
        {
          students.map((student: any) => {
            return <EntityCard entity={student} setSelectedEntity={setSelectedStudent} setUpdateOpen={setOpenUpdateModal} setInfoOpen={setOpenInfoModal} />
          })
        }
      </Flex>
      <UpdateModal entity={selectedStudent} isOpen={openUpdateModal} setIsOpen={setOpenUpdateModal} />
      <InfoModal entity={selectedStudent} isOpen={openInfoModal} setIsOpen={setOpenInfoModal} />
    </Flex>

  )
}
