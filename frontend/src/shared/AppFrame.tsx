import { Flex } from '@chakra-ui/react'
import React from 'react'

export const AppFrame: React.FC<{ children: any }> = ({ children }) => {
  return (
    <Flex h="100vh" w="full" direction="column">{children}</Flex>
  )
}

