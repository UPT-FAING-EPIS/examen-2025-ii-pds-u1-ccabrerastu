import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { getExamById } from "../api"; // importa tu servicio

export default function TakeExam() {
  const { id } = useParams();
  const [exam, setExam] = useState(null);

  useEffect(() => {
    getExamById(id)
      .then(setExam)
      .catch(console.error);
  }, [id]);

  if (!exam) return <div>Cargando...</div>;

  return (
    <div style={{ padding: 20 }}>
      <h2>{exam.title}</h2>
      <p>{exam.description}</p>
      <p>Duración: {exam.durationMinutes} minutos</p>
      {/* Aquí iría la navegación entre preguntas y temporizador */}
    </div>
  );
}
