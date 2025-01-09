import { useQuery } from '@tanstack/react-query';
import { Link } from 'react-router-dom';
import Loading from '../Loading';
import { getCategories } from '../../services/api/CategoryService';

function CategoriesList() {
    const { data: categories, isLoading, isError } = useQuery({
        queryKey: ['categories'],
        queryFn: getCategories,
    });

    if (isError) {
        return <p>Error loading categories</p>;
    }

    if (isLoading) {
        return (
            <Loading />
        );
    }

    return (
        <div className="grid gap-4 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4">
            {categories?.map((parent) =>
                parent.categories?.map((child) => (
                    <Link
                        to={`/${child.slug}`}
                        key={child.categoryId}
                        className="flex items-center rounded-lg border border-gray-200 bg-white px-4 py-2 hover:bg-gray-50 dark:border-gray-700 dark:bg-gray-800 dark:hover:bg-gray-700"
                    >
                        <svg
                            className="me-2 h-4 w-4 shrink-0 text-gray-900 dark:text-white"
                            aria-hidden="true"
                            xmlns="http://www.w3.org/2000/svg"
                            width="24"
                            height="24"
                            fill="none"
                            viewBox="0 0 24 24"
                        >
                            <path
                                stroke="currentColor"
                                strokeLinecap="round"
                                strokeLinejoin="round"
                                strokeWidth="2"
                                d="M12 15v5m-3 0h6M4 11h16M5 15h14a1 1 0 0 0 1-1V5a1 1 0 0 0-1-1H5a1 1 0 0 0-1 1v9a1 1 0 0 0 1 1Z"
                            ></path>
                        </svg>
                        <span className="text-sm font-medium text-gray-900 dark:text-white">
                            {child.categoryName}
                        </span>
                    </Link>
                ))
            )}
        </div>
    );
}

export default CategoriesList;
