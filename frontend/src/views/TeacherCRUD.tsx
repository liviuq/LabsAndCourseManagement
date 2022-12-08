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
import { Teacher } from '../entities/Teacher';
import { UpdateModal } from '../components/UpdateModal';
import { InfoModal } from '../components/InfoModal';
export const TeacherCRUD: React.FC = () => {
  const [openUpdateModal, setOpenUpdateModal] = useState(false);
  const [openInfoModal, setOpenInfoModal] = useState(false);
  const [teachers, setTeachers] = useState([]);
  const [selectedTeacher, setSelectedTeacher] = useState({
    firstName: '',
    lastName: '',
    email: '',
    teachingDegree: ''
  });
  const [teacherCreateInput, setTeacherCreateInput] = useState<Teacher>(
    {
      firstName: '',
      lastName: '',
      email: '',
      teachingDegree: ''
    }
  );
  useEffect(() => {
    console.log(teacherCreateInput)
  }, [teacherCreateInput])
  useEffect(() => {
    fetchTeachers();
  }, [])
  const fetchTeachers = () => {
    Axios.get("https://localhost:7202/api/teachers").then((res) =>
      setTeachers(res.data)
    )
  }
  const handleSubmit = (e: any) => {
    Axios({
      method: "POST",
      url: "https://localhost:7202/api/teachers",
      data: teacherCreateInput,
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
                    setTeacherCreateInput(teacherCreateInput => ({ ...teacherCreateInput, ...{ firstName: event.target.value } }))
                  }} fontSize="lg" textColor="#00ABB3" px="10px" variant='flushed' placeholder='First Name' _placeholder={{ color: "#00ABB3" }} />
                </FormControl>
                <FormControl mb="6">
                  <FormLabel m="0">Last Name</FormLabel>
                  <Input required onChange={(event) => {
                    setTeacherCreateInput(teacherCreateInput => ({ ...teacherCreateInput, ...{ lastName: event.target.value } }))
                  }} fontSize="lg" textColor="#00ABB3" px="10px" variant='flushed' placeholder='Last Name' _placeholder={{ color: "#00ABB3" }} />
                </FormControl>
                <FormControl mb="6">
                  <FormLabel m="0">Email</FormLabel>
                  <Input type="email" required onChange={(event) => {
                    setTeacherCreateInput(teacherCreateInput => ({ ...teacherCreateInput, ...{ email: event.target.value } }))
                  }} fontSize="lg" textColor="#00ABB3" px="10px" variant='flushed' placeholder='Email' _placeholder={{ color: "#00ABB3" }} />
                </FormControl>

                <FormControl mb="6">
                  <FormLabel m="0">Teaching Degree</FormLabel>
                  <Input required onChange={(event) => {
                    setTeacherCreateInput(teacherCreateInput => ({ ...teacherCreateInput, ...{ teachingDegree: event.target.value } }))
                  }} fontSize="lg" textColor="#00ABB3" px="10px" variant='flushed' placeholder='Teaching Degree' _placeholder={{ color: "#00ABB3" }} />
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
          teachers.map((teacher: any) => {
            return <EntityCard entity={teacher} setSelectedEntity={setSelectedTeacher} setUpdateOpen={setOpenUpdateModal} setInfoOpen={setOpenInfoModal} />
          })
        }
      </Flex>
      <UpdateModal entity={selectedTeacher} isOpen={openUpdateModal} setIsOpen={setOpenUpdateModal} />
      <InfoModal entity={selectedTeacher} isOpen={openInfoModal} setIsOpen={setOpenInfoModal} />
    </Flex>

  )
}
