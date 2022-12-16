import { ChakraProvider } from '@chakra-ui/react'
import React, { Children } from 'react'
import ReactDOM from 'react-dom/client'
import {
  createBrowserRouter,
  RouterProvider,
  Route,
} from "react-router-dom";
import { AppFrame } from './shared';
import { BuildTeam, CoursePage, CreateCourse, ErrorPage, Home, Root, StudentCRUD, TeacherCRUD } from './views';

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
  {
    path: "home",
    element: <Home />,
  },
  {
    path: "create-course",
    element: <CreateCourse />,
  },
  {
    path: "build-team",
    element: <BuildTeam />,
  },
  {
    path: "course",
    element: <CoursePage />,
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
