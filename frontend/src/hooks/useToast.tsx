import { AlertStatus, useToast as useToastCK } from "@chakra-ui/react";
import { useCallback } from "react";

interface ToastProps {
  title: string;
  description?: string;
  status: AlertStatus;
}

export function useToast() {
  const toast = useToastCK();

  return useCallback(
    (toastData: ToastProps) => {
      toast({
        variant: 'left-accent',
        title: toastData.title,
        description: toastData.description,
        status: toastData.status,
        duration: 3500,
        position: 'top-right',
        isClosable: true,
        containerStyle: {
          marginRight: '2rem',
        },
      });
    },
    [toast]
  );
}
