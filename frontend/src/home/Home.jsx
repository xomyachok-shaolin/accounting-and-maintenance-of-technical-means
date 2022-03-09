import { Link } from 'react-router-dom';
import { useRecoilValue } from 'recoil';

import { authAtom } from '_state';

export { Home };

function Home() {
    const auth = useRecoilValue(authAtom);

    return (
        <div className="p-4">
            <div className="container">
                <h1>Привет {auth?.firstName}!</h1>
                <p><Link to="/users">Управление пользователями</Link></p>
            </div>
        </div>
    );
}
