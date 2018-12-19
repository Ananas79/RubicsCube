from flask import Flask, request
from sqlalchemy import Column, Integer, String
from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy import create_engine, func
from sqlalchemy.orm import sessionmaker


engine = create_engine('sqlite:///:memory:', echo=False)

app = Flask(__name__)

Base = declarative_base()
class User(Base):
    __tablename__ = 'users'
    id = Column(Integer, primary_key=True)
    login = Column(String)
    password = Column(String)

    def __init__(self, login, password):
        self.login = login
        self.password = password

    def __repr__(self):
        return '{0} ({2})'.format(self.login, self.password)


Base.metadata.create_all(engine)

Session = sessionmaker(bind=engine)
session = Session()

def user_exists(user_login):
    return session.query(User).filter_by(login=user_login).first() is not None

def create_user(login, password):
    return User(login, password)

def add_user(user):
    session.add(user)

def delete_user(user_login):
    session.query(User).filter_by(login=user_login).delete()

def check_password(user_login, user_password):
    return session.query(User.id).filter_by(login=user_login, password=user_password).first() is not None # == password

@app.route('/', methods=['POST'])
def hello_world():
    return 'Hello World!' + str(ourUser.scalar()) #+ str(ourUser in session)


def valid_login(login, password, is_entered):
    if user_exists(login):
        if is_entered == 'True':
            return 'Already entered'
        elif check_password(login, password):
            return 'Enter allowed'
        return 'Wrong password'
    else:
        return 'No user'


@app.route('/login', methods=['POST'])
def log_in():
    return valid_login(request.values['username'], request.values['password'], request.values['is_entered'])
    # the code below is executed if the request method
    # was GET or the credentials were invalid


@app.route('/reg', methods=['POST'])
def reg():
    if user_exists(request.values['username']):
        return 'Exists'
    else:
        user = User(request.values['username'], request.values['password'])
        session.add(user)
        return 'Reged'
    # the code below is executed if the request method
    # was GET or the credentials were invalid