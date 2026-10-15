using System;
using System.Collections.Generic;

class Orang
{
    public string Nama { get; set; }
    public int Umur { get; set; }

    public Orang(string nama, int umur) { Nama = nama; Umur = umur; }

    public virtual void Aktivitas() => Console.WriteLine($"{Nama} sedang beraktivitas.");
    public virtual void InfoOrang() => Console.WriteLine($"Nama: {Nama}, Umur: {Umur}");
}

class Pasien : Orang
{
    public string Keluhan { get; set; }
    public Pasien(string nama, int umur, string keluhan) : base(nama, umur) { Keluhan = keluhan; }

    public void CekKeluhan() => Console.WriteLine($"{Nama} mengeluh: {Keluhan}");
    public override void Aktivitas() => Console.WriteLine($"{Nama} (pasien) sedang menunggu pemeriksaan.");
    public override void InfoOrang() { base.InfoOrang(); Console.WriteLine($"Keluhan: {Keluhan}"); }
}

class TenagaMedis : Orang
{
    public string Spesialisasi { get; set; }
    public TenagaMedis(string nama, int umur, string spesialisasi) : base(nama, umur) { Spesialisasi = spesialisasi; }

    public void CekSpesialis() => Console.WriteLine($"Spesialisasi {Nama}: {Spesialisasi}");
    public override void Aktivitas() => Console.WriteLine($"{Nama} (tenaga medis) sedang bertugas.");
    public override void InfoOrang() { base.InfoOrang(); Console.WriteLine($"Spesialisasi: {Spesialisasi}"); }
}

class Dokter : TenagaMedis
{
    public Dokter(string nama, int umur, string spesialisasi) : base(nama, umur, spesialisasi) { }
    public void Diagnosa() => Console.WriteLine($"Dr. {Nama} sedang mendiagnosa pasien.");
    public override void Aktivitas() => Console.WriteLine($"Dr. {Nama} sedang memeriksa pasien.");
}

class Perawat : TenagaMedis
{
    public Perawat(string nama, int umur, string spesialisasi) : base(nama, umur, spesialisasi) { }
    public void CekPasien() => Console.WriteLine($"Perawat {Nama} sedang mengecek kondisi pasien.");
    public override void Aktivitas() => Console.WriteLine($"Perawat {Nama} sedang merawat pasien.");
}

class PasienAnak : Pasien
{
    public PasienAnak(string nama, int umur, string keluhan) : base(nama, umur, keluhan) { }
    public void Menangis() => Console.WriteLine($"{Nama} menangis karena takut disuntik.");
    public override void Aktivitas() => Console.WriteLine($"{Nama} (anak) sedang bermain sambil menunggu.");
}

class PasienDewasa : Pasien
{
    public PasienDewasa(string nama, int umur, string keluhan) : base(nama, umur, keluhan) { }
    public void Konsultasi() => Console.WriteLine($"{Nama} sedang berkonsultasi dengan dokter.");
    public override void Aktivitas() => Console.WriteLine($"{Nama} (dewasa) sedang mengisi formulir pendaftaran.");
}

class RumahSakit
{
    private List<Orang> daftarOrang = new List<Orang>();

    public void TambahOrang(Orang orang) => daftarOrang.Add(orang);

    public void DaftarOrang()
    {
        foreach (var o in daftarOrang) { o.InfoOrang(); Console.WriteLine("---"); }
    }

    public IEnumerable<Orang> Semua => daftarOrang;
}

class Program
{
    static void Main()
    {
        RumahSakit rs = new RumahSakit();

        Dokter dokter = new Dokter("Budi", 40, "Bedah");
        Perawat perawat = new Perawat("Siti", 28, "Perawat Umum");
        PasienAnak anak = new PasienAnak("Dina", 7, "Demam");
        PasienDewasa dewasa = new PasienDewasa("Andi", 35, "Sakit kepala");

        rs.TambahOrang(dokter);
        rs.TambahOrang(perawat);
        rs.TambahOrang(anak);
        rs.TambahOrang(dewasa);

        Console.WriteLine("=== DAFTAR ORANG ===");
        rs.DaftarOrang();

        Console.WriteLine("=== POLYMORPHISM ===");
        foreach (Orang o in rs.Semua) o.Aktivitas();

        Console.WriteLine("=== METHOD KHUSUS ===");
        dokter.Diagnosa();
        perawat.CekPasien();
        anak.Menangis();
        dewasa.Konsultasi();
        dokter.CekSpesialis();
        anak.CekKeluhan();

        Console.WriteLine("=== KASUS SOAL ===");
        dokter.Aktivitas();            // No. 1
        ((Pasien)anak).Aktivitas();    // No. 1 (pasien)
        dokter.Diagnosa();             // No. 2
        dokter.InfoOrang();            // No. 3
        anak.Menangis();               // No. 4
        Orang o5 = new Perawat("Rina", 30, "ICU");
        o5.Aktivitas();                // No. 5
    }
}