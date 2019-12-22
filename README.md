# tugas-MinimaFinder
Dalam Program.cs
Pada watershed2, telah dibuat algoritma untuk menemukan titik minimal lokal (lokal minima) bernama markLocalMinima. Algoritma tersebut terpasang pada sebuah objek kelas bernama Matrix, sehingga perlu membuat sebuah objek Matrix sebelum dapat dijalankan.
Untuk membuat sebuah objek Matrix, dapat dilakukan secara acak atau manual.

Acak:
"Matrix nama = new Matrix(ukuran, batas bawah, batas atas);"
Manual:
"Matrix nama new Matrix(Matrix.generate(ukuran));"
Setelah itu akan menunggu untuk input pengguna. Input berupa sebuah baris angka.

Setelah dibuat objek Matrix, jalankan algoritma markLocalMinima.
"nama.markLocalMinima();"

Untuk menunjukkan hasilnya;
"nama.show();"
