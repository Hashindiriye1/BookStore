import { useState, useEffect } from 'react'
import axios from 'axios'

interface Author {
  id: number
  name: string
  email: string
  dateOfBirth: string
  books: Book[]
}

interface Book {
  id: number
  title: string
  authorId: number
}

interface ApiResponse<T> {
  isSuccess: boolean
  message: string
  data: T
  errors: string[]
}

function App() {
  const [authors, setAuthors] = useState<Author[]>([])
  const [newAuthor, setNewAuthor] = useState('')
  const [newBook, setNewBook] = useState({ title: '', authorId: 0 })

  useEffect(() => {
    fetchAuthors()
  }, [])

  const fetchAuthors = async () => {
    try {
      const response = await axios.get<ApiResponse<Author[]>>('http://localhost:5281/api/Authors')
      setAuthors(response.data.data)  // Access the data property of the response
    } catch (error) {
      console.error('Error fetching authors:', error)
    }
  }

  const addAuthor = async () => {
    try {
      await axios.post('http://localhost:5281/api/authors', {
        name: newAuthor,
        email: `${newAuthor.toLowerCase().replace(/\s+/g, '.')}@example.com`, 
        dateOfBirth: new Date().toISOString() 
      })
      setNewAuthor('')
      fetchAuthors()
    } catch (error) {
      console.error('Error adding author:', error)
    }
  }

  const deleteAuthor = async (id: number) => {
    try {
      await axios.delete(`http://localhost:5281/api/authors/${id}`)
      fetchAuthors()
    } catch (error) {
      console.error('Error deleting author:', error)
    }
  }

  const addBook = async () => {
    if (!newBook.title || !newBook.authorId) {
      console.error('Please fill in all required fields')
      return
    }
    try {
      await axios.post('http://localhost:5281/api/books', {
        title: newBook.title,
        description: 'A new book', 
        price: 0, 
        publishedDate: new Date().toISOString(), 
        authorId: newBook.authorId
      })
      setNewBook({ title: '', authorId: 0 })
      fetchAuthors()
    } catch (error) {
      console.error('Error adding book:', error)
    }
  }

  const deleteBook = async (id: number) => {
    try {
      await axios.delete(`http://localhost:5281/api/books/${id}`)
      fetchAuthors()
    } catch (error) {
      console.error('Error deleting book:', error)
    }
  }

  return (
    <div className="container">
      <h1>BookStore</h1>
      
      <div className="section">
        <h2>Add Author</h2>
        <div className="input-group">
          <input
            value={newAuthor}
            onChange={(e) => setNewAuthor(e.target.value)}
            placeholder="Author name"
          />
          <button onClick={addAuthor}>Add Author</button>
        </div>
      </div>

      <div className="section">
        <h2>Add Book</h2>
        <div className="input-group">
          <input
            value={newBook.title}
            onChange={(e) => setNewBook({ ...newBook, title: e.target.value })}
            placeholder="Book title"
          />
          <select
            value={newBook.authorId}
            onChange={(e) => setNewBook({ ...newBook, authorId: Number(e.target.value) })}
          >
            <option value={0}>Select Author</option>
            {authors.map(author => (
              <option key={author.id} value={author.id}>{author.name}</option>
            ))}
          </select>
          <button onClick={addBook}>Add Book</button>
        </div>
      </div>

      <div className="section">
        <h2>Authors and Books</h2>
        {authors.map(author => (
          <div key={author.id} className="author-card">
            <h3>
              {author.name}
              <button className="delete-button" onClick={() => deleteAuthor(author.id)}>Delete Author</button>
            </h3>
            <p>Email: {author.email}</p>
            <p>Date of Birth: {new Date(author.dateOfBirth).toLocaleDateString()}</p>
            <ul className="book-list">
              {author.books && author.books.map(book => (
                <li key={book.id} className="book-item">
                  {book.title}
                  <button className="delete-button" onClick={() => deleteBook(book.id)}>Delete Book</button>
                </li>
              ))}
            </ul>
          </div>
        ))}
      </div>
    </div>
  )
}

export default App
