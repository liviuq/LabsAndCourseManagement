import { Center, Flex, Heading, Text } from '@chakra-ui/react'
import React from 'react'
import { useRouteError } from 'react-router-dom';

export const ErrorPage: React.FC = () => {
  const error: any = useRouteError();
  return (
    <Center h="full">
      <Flex direction="column" align="center" justify="center" gap="40px">
        <Heading as="h2" size="3xl" > Oops! </Heading>
        <Text fontSize="lg" fontWeight="semibold">Sorry, an unexpected error has occurred.</Text>
        <Text as="i" fontSize="xl" color="gray" >{error.statusText || error.message}</Text>
      </Flex>
    </Center>

  )
}
