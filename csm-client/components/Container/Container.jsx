import { container } from './Container.module.css';

export default function Container({ children, className }) {
    return <div className={container}>
        {children}
    </div>
}