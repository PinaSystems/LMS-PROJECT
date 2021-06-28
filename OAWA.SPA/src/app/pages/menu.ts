export let MENU_ITEM = [
    {
        path: 'index',
        title: 'Dashboard',
        icon: 'dashboard'
    },
    {
        path: 'content',
        title: 'Course',
        icon: 'pencil',
        children: [
            {
                path: 'create',
                title: 'Upload Content',
            },
            {
                path: 'buttons',
                title: 'Edit Content'
            },
            {
                path: 'view',
                title: 'View Content'
            },
            {
                path: 'tabs',
                title: 'Catalogue'
            },
            {
                path: 'file-tree',
                title: 'Bookmarks'
            },
            {
                path: 'modals',
                title: 'Live Classroom'
            },
            {
                path: 'progress-bar',
                title: 'Feedback'
            },
        ]
    },
    {
        path: 'icon',
        title: 'Elibrary',
        icon: 'diamond',
        children: [
            {
                path: 'grid',
                title: 'Search eLibrary'
            },
            {
                path: 'buttons',
                title: 'View eBooks'
            }
        ]
    },
    {
        path: 'assignment',
        title: 'Assignment',
        icon: 'user',
        children: [
            {
                path: 'upload',
                title: 'Upload Assignment'
            },
            {
                path: 'view',
                title: 'View Assignment'
            },
            {
                path: 'scoring/:id',
                title: 'Scoring assessment'
            }
        ]
    },
    {
        path: 'newsletter',
        title: 'Newsletter',
        icon: 'pencil',
        children: [
            {
                path: 'grid',
                title: 'Upload submissions'
            },
            {
                path: 'upload',
                title: 'Upload Newsleter'
            },
            {
                path: 'view',
                title: 'View Newsletter'
            }
        ]
    },
    {
        path: 'form',
        title: 'Reports',
        icon: 'check-square-o'
    },
    {
        path: 'chat',
        title: 'Chat',
        icon: 'bar-chart'
    },
    {
        path: 'table',
        title: 'Sales',
        icon: 'table'
    },
    {
        path: 'menu-levels',
        title: 'Logout',
        icon: 'sitemap',
    }
];
