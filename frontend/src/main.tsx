import { ChakraProvider } from '@chakra-ui/react'
import axios from 'axios';
import { createEntityStore } from 'entity-of';
import React, { Children } from 'react'
import ReactDOM from 'react-dom/client'
import { QueryClient, QueryClientProvider } from 'react-query';
import {
  createBrowserRouter,
  RouterProvider,
  Route,
} from "react-router-dom";
import { AppFrame } from './shared';
import { CourseView, ErrorPage, Root } from './views';
import { Tutorial } from './views/Tutorial';

createEntityStore()

const router = createBrowserRouter([
  {
    path: "/",
    element: <Root />,
    errorElement: <ErrorPage />,
    children: [
      {
        path: "course/:id",
        element: <CourseView />,
      }
    ],
  },
  {
    path: "tutorial",
    element: <Tutorial />,
  },


]);

axios.defaults.baseURL = 'https://localhost:7202/api'

ReactDOM.createRoot(document.getElementById('root') as HTMLElement).render(
  <React.StrictMode>
    <ChakraProvider>
      <QueryClientProvider client={new QueryClient()}>
        <AppFrame>
          <RouterProvider router={router} />
        </AppFrame>
      </QueryClientProvider>
    </ChakraProvider>
  </React.StrictMode>
)
