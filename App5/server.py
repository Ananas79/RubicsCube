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

class MyBuffer:
    data = ""

    def __init__(self):
        self.__attr = 0

buf = MyBuffer()

Base.metadata.create_all(engine)

Session = sessionmaker(bind=engine)
session = Session()

class FirstNoneException(Exception):
    pass

class WrongPassword(Exception):
    pass

def user_exists(user_login):
    first = session.query(User).filter_by(login=user_login).first()
    if(first == None):
        raise FirstNoneException()
    return True

def create_user(login, password):
    return User(login, password)

def add_user(user):
    session.add(user)

def delete_user(user_login):
    session.query(User).filter_by(login=user_login).delete()

def check_password(user_login, user_password):
    ok = session.query(User.id).filter_by(login=user_login, password=user_password).first()
    if(ok == None):
        raise WrongPassword()
    return True

@app.route('/', methods=['POST'])
def hello_world():
    return 'Hello World!' + str(ourUser.scalar()) #+ str(ourUser in session)


def valid_login(login, password, is_entered):
    if is_entered == 'True':
            return 'Already entered'
    try:
        user_exists(login)
        check_password(login, password)
    except WrongPassword:
        return 'Wrong password'
    except FirstNoneException:
        return 'No user'
    
    return 'Enter allowed'


@app.route('/login', methods=['POST'])
def log_in():
    return valid_login(request.values['username'], request.values['password'], request.values['is_entered'])
    # the code below is executed if the request method
    # was GET or the credentials were invalid


@app.route('/share', methods=['POST'])
def share():
    buf.data = request.values['data']
    return buf.data


@app.route('/load', methods=['GET'])
def load():
    return buf.data

@app.route('/reg', methods=['POST'])
def reg():
    try:
        user_exists(request.values['username'])
        return 'Exists'
    except FirstNoneException:
        user = User(request.values['username'], request.values['password'])
        session.add(user)
        return 'Reged'
    # the code below is executed if the request method
    # was GET or the credentials were invalid