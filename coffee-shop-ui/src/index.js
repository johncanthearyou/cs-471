import React from 'react';
import ReactDOM from 'react-dom/client';
import {
    createBrowserRouter,
    RouterProvider,
} from "react-router-dom";
import { LoginPage, WorkerPage, BaristaPage, CashierPage, ManagerPage } from './Pages';

const router = createBrowserRouter([
    {
        path: "/",
        element: <LoginPage />,
    },
    {
        path: "/worker",
        element: <WorkerPage />,
    },
    {
        path: "/manager",
        element: <ManagerPage />,
    },
    {
        path: "/cashier",
        element: <CashierPage />,
    },
    {
        path: "/barista",
        element: <BaristaPage />,
    },
]);

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
    <>
        <RouterProvider router={router} />
    </>
);
