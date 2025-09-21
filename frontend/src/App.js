import React from 'react'
import { BrowserRouter, Routes, Route } from 'react-router-dom'
import StudentDashboard from './pages/StudentDashboard'
import TakeExam from './pages/TakeExam'


export default function App(){
return (
<BrowserRouter>
<Routes>
<Route path="/" element={<StudentDashboard/>} />
<Route path="/exam/:id" element={<TakeExam/>} />
</Routes>
</BrowserRouter>
)
}