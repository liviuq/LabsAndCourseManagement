import { Flex } from '@chakra-ui/react'
import React from 'react'
import { Outlet } from 'react-router-dom'
import { Header, Sidebar } from '../shared'
export const Root: React.FC = () => {
  return (
    <Flex direction="column" h="100vh">
      <Header />
      <Flex w="full" h="full">
        <Sidebar />
        <Outlet />
      </Flex>
    </Flex>
  )
}

