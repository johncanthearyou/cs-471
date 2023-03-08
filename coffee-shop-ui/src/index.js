import React from 'react';
import ReactDOM from 'react-dom/client';
import {
    createBrowserRouter,
    RouterProvider,
} from "react-router-dom";
import { BaristaPage, CashierPage, LoginPage, ManagerPage } from './Pages';

const router = createBrowserRouter([
    {
        path: "/",
        element: <LoginPage />,
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
    <RouterProvider router={router} />
);
