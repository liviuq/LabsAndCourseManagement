import { ChakraProvider } from '@chakra-ui/react'
import React, { Children } from 'react'
import ReactDOM from 'react-dom/client'
import {
  createBrowserRouter,
  RouterProvider,
  Route,
} from "react-router-dom";
import { AppFrame } from './shared';
import { ErrorPage, Root, StudentCRUD, TeacherCRUD } from './views';

const router = createBrowserRouter([
  {
    path: "/",
    element: <Root />,
    errorElement: <ErrorPage />,
    children: [
      {
        path: "teacher",
        element: <TeacherCRUD />,
      },
      {
        path: "student",
        element: <StudentCRUD />,
      },
    ],
  },
]);

ReactDOM.createRoot(document.getElementById('root') as HTMLElement).render(
  <React.StrictMode>
    <ChakraProvider>
      <AppFrame>
        <RouterProvider router={router} />
      </AppFrame>
    </ChakraProvider>
  </React.StrictMode>
)
