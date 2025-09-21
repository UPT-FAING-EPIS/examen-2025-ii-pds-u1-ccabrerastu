import React, { useEffect, useState } from 'react'
import { Link } from 'react-router-dom'


export default function StudentDashboard(){
const [exams, setExams] = useState([])
useEffect(()=>{
fetch('/api/exams').then(r=>r.json()).then(setExams)
},[])
return (
<div style={{padding:20}}>
<h1>Exámenes asignados</h1>
<ul>{exams.map(e=> <li key={e.id}><Link to={`/exam/${e.id}`}>{e.title}</Link></li>)}</ul>
</div>
)
}