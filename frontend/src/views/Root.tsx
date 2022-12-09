import { Flex } from '@chakra-ui/react'
import React from 'react'
import { Outlet } from 'react-router-dom'
import { Header, Sidebar } from '../shared'
export const Root: React.FC = () => {
  return (
    <>
      <Header />
      <Flex w="full">
        <Sidebar />
        <Outlet />
      </Flex>
    </>
  )
}

