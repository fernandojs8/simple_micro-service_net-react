import { useEffect, useMemo, useState } from "react";

type Sale = {
  id: number;
  title: string;
  price: number;
  category: string;
  date: string;
};

export default function App() {
  const [data, setData] = useState<Sale[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    fetch("http://localhost:5000/api/sales")
      .then((r) => r.json())
      .then((json: Sale[]) => setData(json))
      .catch((e) => setError(String(e)))
      .finally(() => setLoading(false));
  }, []);

  if(loading){
    return <p>Loading...</p>
  }

  if(error){
    return <p style={{ color: "red"}}> {error}</p>
  }

  return (
    <div style={{ padding: 16, fontFamily: "system-ui, sans-serif" }}>
      <h2>Sales</h2>

      <table>
        <thead>
          <tr>
            <th>id</th>
            <th>title</th>
            <th>price</th>
            <th>category</th>
            <th>date</th>
          </tr>
        </thead>
        <tbody>
          {data.map((s) => (
            <tr key={s.id}>
              <td>{s.id}</td>
              <td>{s.title}</td>
              <td>{s.price}</td>
              <td>{s.category}</td>
              <td>{s.date}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}