const username = 'username';
const password = 'password';

export function LoginPage() {
  function loginHandler(event) {
    const username = event.target.username.value;
    const password = event.target.password.value;
    
    alert('username value: ' + username);
    alert('password value: ' + password);
  }

  return (
    <form onSubmit={loginHandler}>
      <label>
        {username}: <input type="text" id={username} required={true} />
      </label>
      <br />
      <label>
        {password}: <input type="current-password" id={password} required={true} />
      </label>
      <br />
      <button type="submit">Login</button>
    </form>
  );
}