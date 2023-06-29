import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'

// https://vitejs.dev/config/
export default defineConfig({
    //vite與vue-cli名稱不一樣
    // base:"/www.livinginvillage.com.tw/rms/",//設定history要設定絕對路徑
    base:"./",//設定history要設定絕對路徑
    server: {
        host: "localhost",
        port: 8080, // 埠號
        open: true, // 配置自動啟動瀏覽器
        https: false,
        // open: true,
        proxy: {   
            '/api/api/': {
                target: 'https://localhost:7016',
                changeOrigin: true,
                secure: false,
                headers: {                  
                    Referer: 'https://localhost:7016', 
                },
                rewrite: (path) => path.replace(/^\/api/, '')
            }
            // '/rms/api/api/': {
            //     // target: 'https://localhost:7016',
            //     target: 'https://livinginvillage.com.tw', 
            //     changeOrigin: true,  //���\���
            //     rewrite: (path) => path.replace(/^\/api/, '')
            // }
        },
    },
    plugins: [vue()],
  // base: '/design/rms/',
})
