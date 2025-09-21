import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { getExams } from '../api';

export default function StudentDashboard() {
  const [exams, setExams] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    getExams()
      .then((data) => setExams(data))
      .catch((err) => setError(err.message))
      .finally(() => setLoading(false));
  }, []);

  if (loading)
    return (
      <div style={styles.centered}>
        <div style={styles.loading}>Cargando exámenes...</div>
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
      <h1 style={styles.title}>Exámenes asignados</h1>

      {exams.length === 0 && <p style={styles.noExams}>No tienes exámenes asignados por ahora.</p>}

      <div style={styles.grid}>
        {exams.map((exam) => (
          <div key={exam.id} style={styles.card}>
            <h2 style={styles.cardTitle}>{exam.title}</h2>
            <p style={styles.cardDescription}>{exam.description || 'Sin descripción'}</p>
            <div style={styles.cardInfo}>
              <p>Duración: {exam.durationMinutes} minutos</p>
              <p>
                Disponible desde:{' '}
                {exam.availableFrom ? new Date(exam.availableFrom).toLocaleString() : 'Indefinido'}
              </p>
              <p>
                Disponible hasta:{' '}
                {exam.availableTo ? new Date(exam.availableTo).toLocaleString() : 'Indefinido'}
              </p>
            </div>
            <Link to={`/exam/${exam.id}`} style={styles.button}>
              Empezar examen
            </Link>
          </div>
        ))}
      </div>
    </div>
  );
}

const styles = {
  container: {
    padding: '40px 20px',
    fontFamily: "'Segoe UI', Tahoma, Geneva, Verdana, sans-serif",
    backgroundColor: '#f5f7fa',
    minHeight: '100vh',
  },
  title: {
    fontSize: 32,
    fontWeight: '700',
    color: '#333',
    marginBottom: 24,
  },
  noExams: {
    color: '#666',
    fontSize: 16,
  },
  grid: {
    display: 'flex',
    flexWrap: 'wrap',
    gap: 24,
  },
  card: {
    backgroundColor: '#fff',
    borderRadius: 12,
    boxShadow: '0 4px 16px rgba(0,0,0,0.1)',
    padding: 20,
    flex: '1 1 300px',
    maxWidth: 350,
    transition: 'transform 0.2s, box-shadow 0.2s',
  },
  cardTitle: {
    fontSize: 20,
    fontWeight: 600,
    color: '#222',
    marginBottom: 8,
  },
  cardDescription: {
    fontSize: 14,
    color: '#555',
    marginBottom: 12,
  },
  cardInfo: {
    fontSize: 13,
    color: '#666',
    marginBottom: 16,
    lineHeight: 1.5,
  },
  button: {
    display: 'block',
    textAlign: 'center',
    padding: '10px 0',
    backgroundColor: '#4f46e5',
    color: '#fff',
    fontWeight: 600,
    borderRadius: 8,
    textDecoration: 'none',
    transition: 'background-color 0.2s',
  },
  centered: {
    display: 'flex',
    justifyContent: 'center',
    alignItems: 'center',
    minHeight: '100vh',
  },
  loading: {
    fontSize: 18,
    color: '#555',
  },
  error: {
    fontSize: 18,
    color: '#e53935',
    fontWeight: 600,
  },
};
