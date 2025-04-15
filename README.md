# NATS Subscriber Application

This project is a C# .NET 9 console application that listens to messages published to a NATS subject, processes them, and persists them into a PostgreSQL database. It demonstrates a clean **3-layered architecture** and is set up to run locally.

---

## 🧱 3-Layered Architecture

**1. API Layer (Subscriber Entry Point)**
- Handles connection to the NATS messaging service.
- Subscribes to the `messages` subject and listens for incoming messages.

**2. Business Layer**
- Contains business logic for handling and transforming messages.
- Validates and prepares messages for persistence.

**3. Data Layer**
- Communicates with PostgreSQL.
- Persists validated messages to the `messages` table.

---

## 🚀 Getting Started

The application is intended to be run locally. Below are the instructions for setting up the components manually.

---

## 🛠️ Manual Setup

### 1. Install PostgreSQL

Make sure PostgreSQL is installed on your local machine. Create a database named `NatsSubscriberDB` and a `messages` table:

```sql
CREATE TABLE messages (
  id SERIAL PRIMARY KEY,
  content TEXT NOT NULL,
  receive_date TIMESTAMP NOT NULL
);
```

### 2. Start NATS Server

Install and start the NATS server. You can run it with the following command:

```bash
nats-server
```

### 3. Configure `appsettings.json`

Edit `src/API/appsettings.json` to include your PostgreSQL connection string:

```json
{
  "ConnectionStrings": {
    "Default": "Host=localhost;Port=5432;Database=NatsSubscriberDB;Username=postgres;Password=1234"
  }
}
```


### 4. Build and Run the Application

Navigate to the API layer of the project and run the application:

```bash
cd src/API
dotnet run
```

The application will start, subscribe to the `messages` subject in NATS, and begin processing incoming messages.

---

## 📁 Project Structure

```
.
NatsSubscriber/
├── src/
│   ├── API/                      
│   │   ├── Services/
│   │   │   └── NatsSubscriberService.cs 
│   │   └── Program.cs            
│   │
│   ├── Business/                 
│   │   └── Services/
│   │       ├── Implementation/
│   │       │   └── MessageProcessor.cs
│   │       └── IMessageProcessor.cs
│   │
│   └── Data/                    
│       ├── Entities/
│       │   └── Message.cs        
│       └── Repositories/
│           ├── Implementation/
│           │   └── MessageRepository.cs
│           └── IMessageRepository.cs  
│
├── test/                       
│   └── [Test files that do not currently work]
├── Dockerfile                  # (Currently not functional)
├── docker-compose.yml          # (Currently not functional)
├── README.md                   
```

---

## 📤 Sending Test Messages

Once the application is running, you can send test messages to the NATS subject to see the app process and save them to the database.

Use the `nats` CLI to publish a test message:

```bash
nats pub messages "Hello from NATS"
```

You should see the following output in the application logs:

```
[BUSINESS] Processed message: 'Hello from NATS'
[DATA] Saved message: 'Hello from NATS'
```

---

## 💡 Notes

- The app subscribes to the `messages` subject in NATS.
- PostgreSQL credentials can be configured in `appsettings.json`.
- The `test` folder and Docker files exist in the GitHub repository but are currently not functional.

---

## 🧼 Cleanup

To stop the application, simply press `Ctrl+C` in the terminal.
