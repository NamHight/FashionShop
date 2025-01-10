import { useQuery } from '@tanstack/react-query';
import { Link } from 'react-router';
import { useState } from 'react';
import Loading from '../Loading';
import { getCategories } from '../../services/api/CategoryService';


function CategoriesList() {
    const { data: categories, isLoading, isError } = useQuery({
        queryKey: ['categories'],
        queryFn: async () => {
            var result = await getCategories();
            return result;
        },
        refetchIntervalInBackground: false,
        refetchOnWindowFocus: false
    });

    const [showAll, setShowAll] = useState(false); // State để kiểm soát việc hiển thị toàn bộ

    if (isError) {
        return <p>Error loading categories</p>;
    }

    if (isLoading) {
        return <Loading />;
    }
    // Gộp tất cả child categories lại
    const allChildCategories = categories.data?.map(parent => parent.categories).flat() || [];
    // Lọc danh sách theo trạng thái "showAll"
    const displayedCategories = showAll ? allChildCategories : allChildCategories.slice(0, 12);

    return (
        <div>
            <div className="grid gap-4 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4">
                {displayedCategories.map((child) => (
                    <Link
                        to={`/${child.slug}`}
                        key={child.categoryId}
                        className="flex items-center rounded-lg border border-gray-200 bg-white px-4 py-2 hover:bg-gray-50 dark:border-gray-700 dark:bg-gray-800 dark:hover:bg-gray-700"
                    >
                        <svg 
                            xmlns="http://www.w3.org/2000/svg" 
                            fill="none" 
                            viewBox="0 0 24 24"
                            strokeWidth="1.5"
                            stroke="#1E90FF" 
                            className="size-4">
                            <path 
                                strokeLinecap="round"
                                strokeLinejoin="round"
                                d="M5.25 8.25h15m-16.5 7.5h15m-1.8-13.5-3.9 19.5m-2.1-19.5-3.9 19.5" 
                            />
                        </svg>
                        <span className="text-sm font-medium text-gray-900 dark:text-white">
                            {child.categoryName}
                        </span>
                    </Link>
                ))}
            </div>
            <div className="text-center mt-4">
                <button
                    onClick={() => setShowAll(!showAll)}
                    className="px-4 py-2 bg-emerald-400 text-white rounded hover:bg-blue-600 dark:bg-blue-700 dark:hover:bg-blue-800"
                >
                    {showAll ? 'Show Less' : 'Show More'}
                </button>
            </div>
        </div>
    );
}

export default CategoriesList;
