import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { getExamById } from "../api";

export default function TakeExam() {
  const { id } = useParams();
  const [exam, setExam] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    getExamById(id)
      .then((data) => setExam(data))
      .catch((err) => setError(err.message))
      .finally(() => setLoading(false));
  }, [id]);

  if (loading)
    return (
      <div style={styles.centered}>
        <div style={styles.loading}>Cargando examen...</div>
      </div>
    );

  if (error)
    return (
      <div style={styles.centered}>
        <div style={styles.error}>Error: {error}</div>
      </div>
    );

  return (
    <div style={styles.container}>
      <div style={styles.card}>
        <h1 style={styles.title}>{exam.title}</h1>
        <p style={styles.description}>{exam.description || "Sin descripción"}</p>
        <div style={styles.info}>
          <p>Duración: {exam.durationMinutes} minutos</p>

        </div>
        
      </div>
    </div>
  );
}

const styles = {
  container: {
    padding: "40px 20px",
    minHeight: "100vh",
    display: "flex",
    justifyContent: "center",
    alignItems: "flex-start",
    backgroundColor: "#f5f7fa",
    fontFamily: "'Segoe UI', Tahoma, Geneva, Verdana, sans-serif",
  },
  card: {
    backgroundColor: "#fff",
    borderRadius: 12,
    padding: 30,
    maxWidth: 600,
    width: "100%",
    boxShadow: "0 6px 20px rgba(0,0,0,0.1)",
    transition: "transform 0.2s, box-shadow 0.2s",
  },
  title: {
    fontSize: 28,
    fontWeight: 700,
    color: "#222",
    marginBottom: 16,
  },
  description: {
    fontSize: 16,
    color: "#555",
    marginBottom: 20,
  },
  info: {
    fontSize: 14,
    color: "#666",
    marginBottom: 24,
    lineHeight: 1.6,
  },
  button: {
    padding: "12px 0",
    width: "100%",
    backgroundColor: "#4f46e5",
    color: "#fff",
    fontWeight: 600,
    fontSize: 16,
    border: "none",
    borderRadius: 8,
    cursor: "pointer",
    transition: "background-color 0.2s, transform 0.2s",
  },
  centered: {
    display: "flex",
    justifyContent: "center",
    alignItems: "center",
    minHeight: "100vh",
  },
  loading: {
    fontSize: 18,
    color: "#555",
  },
  error: {
    fontSize: 18,
    color: "#e53935",
    fontWeight: 600,
  },
};
