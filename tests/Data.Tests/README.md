NATS Message Subscriber

A .NET application that subscribes to NATS messages and stores them in PostgreSQL.


🏛️ 3-Layer Architecture
    API Layer (/API)
        Entry point and service orchestration
        Hosts NATS subscriber service and receives messages
    Business Layer (/Business)
        Processes and validates messages
    Data Layer (/Data)
        Handles database operations


⚙️ Prerequisites
    .NET 7.0+ SDK
    PostgreSQL 15+ 
    NATS Server


📂 Project Structure
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
└── README.md                    


NATS Server Setup (Local Message Bus)
    1. Install NATS Server
        # Mac (Homebrew)
        brew install nats-server

        # Linux (Snap)
        sudo snap install nats-server

        # Windows (Chocolatey)
        choco install nats-server

    2. Start NATS Server
        # Default mode (development)
        nats-server

        # With monitoring (http://localhost:8222)
        nats-server -m 8222

        Running on nats://localhost:4222 by default

    3. Verify NATS is Running
        # Check server status
        nats server info

        # Send test message
        nats pub test "Hello NATS"

        # Subscribe to test messages
        nats sub test


Run the System
    Start NATS Server (in Terminal 1):
        nats-server

    Run the Subscriber (in Terminal 2):
        dotnet run --project src/API/NatsSubscriber.csproj

    Send Messages (in Terminal 3):
        nats pub messages "Hello, NATS"

    Check PostgreSQL:
        psql -U postgres -d NatsMessagesDB -c "SELECT * FROM messages;"

    ✅ Expected Flow:
        Saved to PostgreSQL automatically
