import { Footer } from '@/Template/Footer'
import { Header } from '@/Template/Header'
import { CallToAction } from '@/Template/CallToAction'
import { Faqs } from '@/Template/Faqs'
import { Hero } from '@/Template/Hero'
import { Pricing } from '@/Template/Pricing'
import { PrimaryFeatures } from '@/Template/PrimaryFeatures'
import { SecondaryFeatures } from '@/Template/SecondaryFeatures'
import { Testimonials } from '@/Template/Testimonials'
import Image from 'next/image'

export default function Home() {
  return (
    <>
      <Header />
      <main>
        <Hero />
        <PrimaryFeatures />
        <SecondaryFeatures />
        <CallToAction />
        <Testimonials />
        <Pricing />
        <Faqs />
      </main>
      <Footer />
    </>
  )
}
