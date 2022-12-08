import { Image, Text, Flex, Box } from '@chakra-ui/react'
import langIcon from "../assets/languageIcon.svg"
export const LangChanger = () => {

  return (
    <Flex align="center">
      <Text fontSize="xs" fontWeight="500" color="#00ABB3" cursor="pointer">RO</Text>
      <Box bg="#00ABB3" w="2px" h="12px" mx="3px" />
      <Text fontSize="xs" fontWeight="500" cursor="pointer">EN</Text>
      <Image src={langIcon} alt='' ml="10px" />
    </Flex>
  )
}


