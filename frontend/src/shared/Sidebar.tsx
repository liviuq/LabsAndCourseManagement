import { Button, Flex, Heading, Text } from "@chakra-ui/react";
import { useNavigate } from "react-router-dom";
import React, { useEffect, useState } from "react";
import { useGetCourses } from "../cache/get";
import { Course } from "../entities";
import { useQuery } from "react-query";
import { SidebarElement } from ".";
import { FiPlus } from "react-icons/fi";
import { CreateCourseModal } from "../components/forms";

export const Sidebar: React.FC = () => {
  const navigate = useNavigate();
  const [courses, setCourses] = useState<Course[]>();
  const coursesData = useQuery(['getCourses'], () => useGetCourses()).data
  const [isOpenCreateCourseModal, setIsOpenCreateCourseModal] = useState(false);

  useEffect(() => {
    setCourses(coursesData ? coursesData : [])
  }, [coursesData])

  return (
    <Flex minW="330px" bg="#EAEAEA" direction="column" p="32px" position="relative">
      <Flex justify="space-between" align="flex-start" mb="15px">
        <Heading size="md" color="#3C4048" mt="2px" >My courses</Heading>
        <Button onClick={() => setIsOpenCreateCourseModal(true)} variant="ghost" size="sm" leftIcon={<FiPlus />} colorScheme="teal">Add</Button>
      </Flex>
      <Flex pl="20px" gap="10px" direction="column">
        {
          courses?.map((course) => <SidebarElement key={course.id} course={course} />)
        }
      </Flex>
      <Button onClick={() => navigate('/tutorial')} position="absolute" bottom="24px" left="32px" variant="link" colorScheme="teal">Tutorial</Button>
      <CreateCourseModal isOpen={isOpenCreateCourseModal} setIsOpen={setIsOpenCreateCourseModal} />
    </Flex>
  );
};
